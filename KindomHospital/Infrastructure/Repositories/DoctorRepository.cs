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

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyIdAsync(int specialtyId)
        {
            return await context.Doctors
                .Where(d => d.SpecialtyId == specialtyId)
                .ToListAsync();
        }

        public async Task<Specialty?> GetSpecialtyByDoctorIdAsync(int doctorId)
        {
            var doctor = await context.Doctors
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId);

            return doctor?.Specialty;
        }

        public async Task<int> ChangeDoctorSpecialtyAsync(int doctorId, int specialtyId)
        {
            var specialtyExists = await context.Specialties.AnyAsync(s => s.SpecialtyId == specialtyId);
            if (!specialtyExists)
                return -1;

            var existing = await context.Doctors.FindAsync(doctorId);
            if (existing is null)
                return 0;

            existing.SpecialtyId = specialtyId;
            await context.SaveChangesAsync();
            return 1;
        }

        public async Task<IEnumerable<Consultation>> GetConsultationsByDoctorAsync(int doctorId, DateOnly? from, DateOnly? to)
        {
            var query = context.Consultations.Where(c => c.DoctorId == doctorId);

            if (from.HasValue)
                query = query.Where(c => c.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(c => c.Date <= to.Value);

            return await query
                .OrderBy(c => c.Date)
                .ThenBy(c => c.Hour)
                .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetPatientsByDoctorAsync(int doctorId)
        {
            var patientIds = await context.Consultations
                .Where(c => c.DoctorId == doctorId)
                .Select(c => c.PatientId)
                .Distinct()
                .ToListAsync();

            if (!patientIds.Any())
                return Array.Empty<Patient>();

            return await context.Patients
                .Where(p => patientIds.Contains(p.PatientId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Ordonnance>> GetOrdonnancesByDoctorAsync(int doctorId, DateOnly? from, DateOnly? to)
        {
            var query = context.Ordonnances.Where(o => o.DoctorId == doctorId);

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