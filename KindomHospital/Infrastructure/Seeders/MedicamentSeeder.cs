using KindomHospital.Domain.Entities;
using KindomHospital.Infrastructure.Seeders.Helpers;
using KindomHospital.Infrastructure.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Seeders
{
    public class MedicamentSeeder : ISeeder
    {
        private readonly KingdomHospitalContext _context;
        private readonly IWebHostEnvironment _env;

        public MedicamentSeeder(KingdomHospitalContext context, IWebHostEnvironment env) 
        {
            _context = context;
            _env = env;
        }

        public async Task Seed()
        {
            if (await _context.Medicaments.AnyAsync())
                return;
            var path = Path.Combine(
                _env.ContentRootPath,
                "Infrastructure",
                "Seeders",
                "Csv",
                "Medicament.csv"
            );
            var rows = (await CsvReader.ReadCsv(path)).Skip(1);
            foreach (var row in rows)
            {
                _context.Medicaments.Add(new Medicament
                {
                    Name = row[1].Trim(),
                    DosageForm = row[2].Trim(),
                    Strength = row[3].Trim(),
                    AtcCode = row[4].Trim()
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
