using KindomHospital.Application.Repositories;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class OrdonnanceRepository(KingdomHospitalContext context) : IOrdonnanceRepository
    {
        public async Task<IEnumerable<Ordonnance>> GetAllOrdonnancesAsync()
        {
            return await context.Ordonnances.ToListAsync();
        }

        public async Task<int> AddOrdonnanceAsync(Ordonnance ordonnance)
        {
            var doctorExists = await context.Doctors.AnyAsync(d => d.DoctorId == ordonnance.DoctorId);
            var patientExists = await context.Patients.AnyAsync(p => p.PatientId == ordonnance.PatientId);

            if (!doctorExists || !patientExists)
                return -1;

            if (ordonnance.ConsultationId.HasValue)
            {
                var consultationExists = await context.Consultations.AnyAsync(c => c.ConsultationId == ordonnance.ConsultationId.Value);
                if (!consultationExists)
                    return -1;
            }

            await context.Ordonnances.AddAsync(ordonnance);
            await context.SaveChangesAsync();
            return ordonnance.OrdonnanceId;
        }

        public async Task<Ordonnance> GetOrdonnanceById(int id)
        {
            return await context.Ordonnances.FirstOrDefaultAsync(o => o.OrdonnanceId == id);
        }

        public async Task<int> UpdateOrdonnanceAsync(Ordonnance ordonnance)
        {
            var doctorExists = await context.Doctors.AnyAsync(d => d.DoctorId == ordonnance.DoctorId);
            var patientExists = await context.Patients.AnyAsync(p => p.PatientId == ordonnance.PatientId);

            if (!doctorExists || !patientExists)
                return -1;

            if (ordonnance.ConsultationId.HasValue)
            {
                var consultationExists = await context.Consultations.AnyAsync(c => c.ConsultationId == ordonnance.ConsultationId.Value);
                if (!consultationExists)
                    return -1;
            }

            var existing = await context.Ordonnances.FindAsync(ordonnance.OrdonnanceId);
            if (existing is null)
                return 0;

            existing.DoctorId = ordonnance.DoctorId;
            existing.PatientId = ordonnance.PatientId;
            existing.ConsultationId = ordonnance.ConsultationId;
            existing.Date = ordonnance.Date;
            existing.Notes = ordonnance.Notes;

            await context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteOrdonnanceAsync(int id)
        {
            var existing = await context.Ordonnances.FindAsync(id);
            if (existing is null)
                return 0;

            context.Ordonnances.Remove(existing);
            await context.SaveChangesAsync();
            return 1;
        }

        public async Task<IEnumerable<Ordonnance>> GetOrdonnancesByConsultationAsync(int consultationId)
        {
            return await context.Ordonnances
                .Where(o => o.ConsultationId == consultationId)
                .OrderBy(o => o.Date)
                .ToListAsync();
        }

        public async Task<int> AttachOrdonnanceToConsultationAsync(int ordonnanceId, int consultationId)
        {
            var consultationExists = await context.Consultations.AnyAsync(c => c.ConsultationId == consultationId);
            if (!consultationExists)
                return -1;

            var existing = await context.Ordonnances.FindAsync(ordonnanceId);
            if (existing is null)
                return 0;

            existing.ConsultationId = consultationId;
            await context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DetachOrdonnanceFromConsultationAsync(int ordonnanceId)
        {
            var existing = await context.Ordonnances.FindAsync(ordonnanceId);
            if (existing is null)
                return 0;

            existing.ConsultationId = null;
            await context.SaveChangesAsync();
            return 1;
        }

        public async Task<IEnumerable<Ordonnance>> GetOrdonnancesFilteredAsync(int? doctorId, int? patientId, DateOnly? from, DateOnly? to)
        {
            var query = context.Ordonnances.AsQueryable();

            if (doctorId.HasValue)
                query = query.Where(o => o.DoctorId == doctorId.Value);

            if (patientId.HasValue)
                query = query.Where(o => o.PatientId == patientId.Value);

            if (from.HasValue)
                query = query.Where(o => o.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(o => o.Date <= to.Value);

            return await query
                .OrderBy(o => o.Date)
                .ToListAsync();
        }
    }
}