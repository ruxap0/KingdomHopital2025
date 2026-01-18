using KindomHospital.Application.Repositories;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class DoctorRepository(KingdomHospitalContext context) : IDoctorRepository
    {
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await context.Doctors.ToListAsync();
        }

        public async Task<int> AddDoctorAsync(Doctor doctor)
        {
            var SpecialtyExists = await context.Specialties.AnyAsync(s => s.SpecialtyId == doctor.SpecialtyId);

            if (SpecialtyExists)
            {
                await context.Doctors.AddAsync(doctor);
                await context.SaveChangesAsync();
                return doctor.DoctorId;
            }
            return -1;
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            return await context.Doctors.FindAsync(id);
        }

        public async Task<int> UpdateDoctorAsync(Doctor doctor)
        { 
            var specialtyExists = await context.Specialties.AnyAsync(s => s.SpecialtyId == doctor.SpecialtyId);
            if (!specialtyExists)
                return -1;

            var existing = await context.Doctors.FindAsync(doctor.DoctorId);
            if (existing is null)
                return 0;

            existing.FirstName = doctor.FirstName;
            existing.LastName = doctor.LastName;
            existing.SpecialtyId = doctor.SpecialtyId;

            await context.SaveChangesAsync();
            return 1;
        }
    }
}