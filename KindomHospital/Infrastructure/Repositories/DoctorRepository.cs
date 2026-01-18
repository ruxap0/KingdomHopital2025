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
    }
}