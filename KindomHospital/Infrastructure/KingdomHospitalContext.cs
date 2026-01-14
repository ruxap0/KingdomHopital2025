using Microsoft.EntityFrameworkCore;
using KindomHospital.Domain.Entities;

namespace KindomHospital.Infrastructure
{
    public class KingdomHospitalContext : DbContext
    {
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Ordonnance> Ordonnances { get; set; }
        public DbSet<OrdonnanceLigne> OrdonnanceLignes { get; set; }

        public KingdomHospitalContext(DbContextOptions<KingdomHospitalContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HospitalKingdomDb;Trusted_Connection=True;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KingdomHospitalContext).Assembly);
        }
    }
}
