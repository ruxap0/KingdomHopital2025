using KindomHospital.Infrastructure.Seeders.Interfaces;
using KindomHospital.Domain.Entities;
using KindomHospital.Infrastructure.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Seeders
{
    public class PatientSeeder : ISeeder
    {
        private readonly KingdomHospitalContext _context;

        public PatientSeeder(KingdomHospitalContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Patients.AnyAsync())
                return;

            var patients = new List<Patient>
            {
                new()
                {
                    FirstName = "Vladimir",
                    LastName = "Ilitch Oulianov",
                    BirthDate = new DateOnly(1980, 5, 12)
                },
                new()
                {
                    FirstName = "Joseph",
                    LastName = "Vissarionovitch Djougachvili",
                    BirthDate = new DateOnly(1992, 11, 3)
                },
                new()
                {
                    FirstName = "Gueorgui",
                    LastName = "Malenkov",
                    BirthDate = new DateOnly(1975, 1, 30)
                },
                new()
                {
                    FirstName = "Nikita",
                    LastName = "Khrouchtchev",
                    BirthDate = new DateOnly(2006, 6, 20)
                },
                new()
                {
                    FirstName = "Léonid",
                    LastName = "Brejnev",
                    BirthDate = new DateOnly(2016, 8, 15)
                }
            };

            foreach (var p in patients)
                _context.Patients.Add(p);

            await _context.SaveChangesAsync();
        }
    }
}