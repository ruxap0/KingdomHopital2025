using KindomHospital.Application.Repositories;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class ConsultationRepository(KingdomHospitalContext context) : IConsultationRepository
    {
        public async Task<IEnumerable<Consultation>> GetAllConsultationsAsync()
        {
            return await context.Consultations.ToListAsync();
        }

        public async Task<int> AddConsultationAsync(Consultation consultation)
        {
            var doctorExists = await context.Doctors.AnyAsync(d => d.DoctorId == consultation.DoctorId);
            var patientExists = await context.Patients.AnyAsync(p => p.PatientId == consultation.PatientId);

            if (doctorExists && patientExists)
            {
                await context.Consultations.AddAsync(consultation);
                await context.SaveChangesAsync();
                return consultation.ConsultationId;
            }

            return -1;
        }

        public async Task<Consultation> GetConsultationById(int id)
        {
            return await context.Consultations.FirstOrDefaultAsync(c => c.ConsultationId == id);
        }

        public async Task<int> UpdateConsultationAsync(Consultation consultation)
        {
            var doctorExists = await context.Doctors.AnyAsync(d => d.DoctorId == consultation.DoctorId);
            var patientExists = await context.Patients.AnyAsync(p => p.PatientId == consultation.PatientId);

            if (!doctorExists || !patientExists)
                return -1;

            var existing = await context.Consultations.FindAsync(consultation.ConsultationId);
            if (existing is null)
                return 0;

            existing.DoctorId = consultation.DoctorId;
            existing.PatientId = consultation.PatientId;
            existing.Date = consultation.Date;
            existing.Hour = consultation.Hour;
            existing.Reason = consultation.Reason;

            await context.SaveChangesAsync();
            return 1;
        }
    }
}