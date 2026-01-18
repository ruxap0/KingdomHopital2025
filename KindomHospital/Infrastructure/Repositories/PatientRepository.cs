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

        public async Task<int> AddPatientAsync(Patient patient)
        {
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
            return patient.PatientId;
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
        }

        public async Task<int> UpdatePatientAsync(Patient patient)
        {
            var existing = await context.Patients.FindAsync(patient.PatientId);
            if (existing is null)
                return 0;

            existing.FirstName = patient.FirstName;
            existing.LastName = patient.LastName;
            existing.BirthDate = patient.BirthDate;

            await context.SaveChangesAsync();
            return 1;
        }
    }
}