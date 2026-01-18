using Serilog;
using KindomHospital.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using KindomHospital.Application.Mappers;
using KindomHospital.Application.Services;
using KindomHospital.Infrastructure.Seeders;
using KindomHospital.Infrastructure.Seeders.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using KindomHospital.Application.Repositories;
using KindomHospital.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSerilog((services, lc) =>
    lc.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Ajouter les Mappers au DI
builder.Services.AddSingleton<SpecialtyMapper>();
builder.Services.AddSingleton<DoctorMapper>();
builder.Services.AddSingleton<PatientMapper>();
builder.Services.AddSingleton<ConsultationMapper>();
builder.Services.AddSingleton<MedicamentMapper>();
builder.Services.AddSingleton<OrdonnanceMapper>();
builder.Services.AddSingleton<OrdonnanceLigneMapper>();

// Ajouter les services au DI
builder.Services.AddScoped<SpecialtyService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<ConsultationService>();
builder.Services.AddScoped<MedicamentService>();
builder.Services.AddScoped<OrdonnanceService>();

// Ajouter les repositories au DI
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();
builder.Services.AddScoped<IMedicamentRepository, MedicamentRepository>();
builder.Services.AddScoped<IOrdonnanceRepository, OrdonnanceRepository>();
builder.Services.AddScoped<IOrdonnanceLigneRepository, OrdonnanceLigneRepository>();

// DbContext (scoped)
builder.Services.AddDbContext<KingdomHospitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISeeder, SpecialtySeeder>();
builder.Services.AddScoped<ISeeder, MedicamentSeeder>();
builder.Services.AddScoped<ISeeder, DoctorSeeder>();
builder.Services.AddScoped<ISeeder, PatientSeeder>();
builder.Services.AddScoped<ISeeder, ConsultationSeeder>();
builder.Services.AddScoped<ISeeder, OrdonnanceSeeder>();

var app = builder.Build();

app.UseSerilogRequestLogging();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<KingdomHospitalContext>();
        await context.Database.MigrateAsync();

        var seeders = services.GetServices<ISeeder>();
        foreach (var seeder in seeders)
        {
            await seeder.Seed();
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Erreur lors de l'initialisation de la base de données (migrations/seeders).");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("KindomHospital API")
            .WithTheme(ScalarTheme.Purple)
            .EnableDarkMode()
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
