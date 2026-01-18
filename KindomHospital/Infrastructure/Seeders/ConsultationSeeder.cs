using KindomHospital.Domain.Entities;
using KindomHospital.Infrastructure.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Seeders
{
    public class ConsultationSeeder : ISeeder
    {
        private readonly KingdomHospitalContext _context;

        public ConsultationSeeder(KingdomHospitalContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Consultations.AnyAsync())
                return;

            var patients = await _context.Patients.OrderBy(p => p.PatientId).ToListAsync();
            var doctors = await _context.Doctors.OrderBy(d => d.DoctorId).ToListAsync();

            if (!patients.Any() || !doctors.Any())
                return;

            var baseDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1));

            var consultations = new List<Consultation>();

            var specialDay = baseDate.AddDays(2);
            var doctorForDuplicateDay = doctors.First();

            consultations.Add(new Consultation
            {
                DoctorId = doctorForDuplicateDay.DoctorId,
                PatientId = patients[0].PatientId,
                Date = specialDay,
                Hour = new TimeOnly(9, 0),
                Reason = "Contrôle général",
                Doctor = doctorForDuplicateDay,
                Patient = patients[0]
            });

            consultations.Add(new Consultation
            {
                DoctorId = doctorForDuplicateDay.DoctorId,
                PatientId = patients[1].PatientId,
                Date = specialDay,
                Hour = new TimeOnly(11, 0),
                Reason = "Suivi",
                Doctor = doctorForDuplicateDay,
                Patient = patients[1]
            });

            int nextId = 3;

            consultations.Add(new Consultation
            {
                DoctorId = doctors[1].DoctorId,
                PatientId = patients[0].PatientId,
                Date = baseDate,
                Hour = new TimeOnly(10, 30),
                Reason = "Douleur thoracique",
                Doctor = doctors[1],
                Patient = patients[0]
            });

            consultations.Add(new Consultation
            {
                DoctorId = doctors[2].DoctorId,
                PatientId = patients[0].PatientId,
                Date = baseDate.AddDays(3),
                Hour = new TimeOnly(14, 0),
                Reason = "Vaccination",
                Doctor = doctors[2],
                Patient = patients[0]
            });

            consultations.Add(new Consultation
            {
                DoctorId = doctors[3].DoctorId,
                PatientId = patients[1].PatientId,
                Date = baseDate.AddDays(1),
                Hour = new TimeOnly(9, 30),
                Reason = "Bilan",
                Doctor = doctors[3],
                Patient = patients[1]
            });

            consultations.Add(new Consultation
            {
                DoctorId = doctors[4].DoctorId,
                PatientId = patients[1].PatientId,
                Date = baseDate.AddDays(4),
                Hour = new TimeOnly(16, 0),
                Reason = "Contrôle tension",
                Doctor = doctors[4],
                Patient = patients[1]
            });

            consultations.Add(new Consultation
            {
                DoctorId = doctors[0].DoctorId,
                PatientId = patients[2].PatientId,
                Date = baseDate.AddDays(5),
                Hour = new TimeOnly(10, 0),
                Reason = "Consultation initiale",
                Doctor = doctors[0],
                Patient = patients[2]
            });

            consultations.Add(new Consultation
            {
                DoctorId = doctors[5 % doctors.Count].DoctorId,
                PatientId = patients[2].PatientId,
                Date = baseDate.AddDays(6),
                Hour = new TimeOnly(11, 30),
                Reason = "Résultats",
                Doctor = doctors[5 % doctors.Count],
                Patient = patients[2]
            });

            consultations.Add(new Consultation
            {
                DoctorId = doctors[1].DoctorId,
                PatientId = patients[3].PatientId,
                Date = baseDate.AddDays(7),
                Hour = new TimeOnly(15, 0),
                Reason = "Allergie",
                Doctor = doctors[1],
                Patient = patients[3]
            });

            consultations.Add(new Consultation
            {
                DoctorId = doctors[2].DoctorId,
                PatientId = patients[4].PatientId,
                Date = baseDate.AddDays(8),
                Hour = new TimeOnly(9, 45),
                Reason = "Pédiatrie - suivi",
                Doctor = doctors[2],
                Patient = patients[4]
            });

            foreach (var c in consultations)
                _context.Consultations.Add(c);

            await _context.SaveChangesAsync();
        }
    }
}