using KindomHospital.Domain.Entities;
using KindomHospital.Infrastructure.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Seeders
{
    public class DoctorSeeder : ISeeder
    {
        private readonly KingdomHospitalContext _context;

        public DoctorSeeder(KingdomHospitalContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Doctors.AnyAsync())
                return;

            var specialties = await _context.Specialties.OrderBy(s => s.SpecialtyId).ToListAsync();
            if (!specialties.Any())
                return;

            var doctors = new List<Doctor>
            {
                new()
                {
                    SpecialtyId = specialties[0].SpecialtyId,
                    FirstName = "Lionel",
                    LastName = "Jospin",
                    Specialty = specialties[0]
                },
                new()
                {
                    SpecialtyId = specialties.Count > 1 ? specialties[1].SpecialtyId : specialties[1].SpecialtyId,
                    FirstName = "Alain",
                    LastName = "Savary",
                    Specialty = specialties.Count > 1 ? specialties[1] : specialties[1]
                },
                new()
                {
                    SpecialtyId = specialties.Count > 2 ? specialties[2].SpecialtyId : specialties[2].SpecialtyId,
                    FirstName = "François",
                    LastName = "Mitterrand",
                    Specialty = specialties.Count > 2 ? specialties[2] : specialties[2]
                },
                new()
                {
                    SpecialtyId = specialties.Count > 3 ? specialties[3].SpecialtyId : specialties[3].SpecialtyId,
                    FirstName = "Pierre",
                    LastName = "Mauroy",
                    Specialty = specialties.Count > 3 ? specialties[3] : specialties[3]
                },
                new()
                {
                    SpecialtyId = specialties.Count > 4 ? specialties[4].SpecialtyId : specialties[4].SpecialtyId,
                    FirstName = "Laurent",
                    LastName = "Fabius",
                    Specialty = specialties.Count > 4 ? specialties[4] : specialties[4]
                },
                new()
                {
                    SpecialtyId = specialties.Count > 5 ? specialties[5].SpecialtyId : specialties[5].SpecialtyId,
                    FirstName = "Olivier",
                    LastName = "Faure",
                    Specialty = specialties.Count > 5 ? specialties[5] : specialties[5]
                }
            };

            foreach (var d in doctors)
                _context.Doctors.Add(d);

            await _context.SaveChangesAsync();
        }
    }
}