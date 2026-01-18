using KindomHospital.Application.Repositories;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class PatientRepository(KingdomHospitalContext context) : IPatientRepository
    {
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await context.Patients.ToListAsync();
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
        }
    }
}