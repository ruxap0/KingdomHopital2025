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

        public async Task AddDoctorAsync(Doctor doctor)
        {
            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
        }
    }
}