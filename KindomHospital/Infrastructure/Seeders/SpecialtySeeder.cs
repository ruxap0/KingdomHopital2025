using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using KindomHospital.Domain.Entities;
using KindomHospital.Infrastructure;
using KindomHospital.Infrastructure.Seeders.Helpers;
using KindomHospital.Infrastructure.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

public class SpecialtySeeder : ISeeder
{
    private readonly KingdomHospitalContext _context;
    private readonly IWebHostEnvironment _env;

    public SpecialtySeeder(KingdomHospitalContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task Seed()
    {
        if (await _context.Specialties.AnyAsync())
            return;

        var path = Path.Combine(
            _env.ContentRootPath,
            "Infrastructure",
            "Seeders",
            "Csv",
            "Specialty.csv"
        );

        var rows = (await CsvReader.ReadCsv(path)).Skip(1);

        var entityType = _context.Model.FindEntityType(typeof(Specialty));
        var nameProperty = entityType?.FindProperty(nameof(Specialty.Name));
        var maxLength = nameProperty?.GetMaxLength() ?? 0;

        var specialties = new List<Specialty>();
        foreach (var row in rows)
        {
            var name = row[1].Trim();

            if (maxLength > 0 && name.Length > maxLength)
            {
                name = name.Substring(0, maxLength);
                Console.WriteLine($"[Seeder] Truncated Specialty Name to {maxLength} chars: '{name}'");
            }

            specialties.Add(new Specialty
            {
                Name = name
            });
        }

        _context.Specialties.AddRange(specialties);
        await _context.SaveChangesAsync();
    }
}