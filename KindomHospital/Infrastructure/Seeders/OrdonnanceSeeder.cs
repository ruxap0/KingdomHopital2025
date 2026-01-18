using KindomHospital.Domain.Entities;
using KindomHospital.Infrastructure.Seeders.Interfaces;

using KindomHospital.Domain.Entities;
using KindomHospital.Infrastructure.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Seeders
{
    public class OrdonnanceSeeder : ISeeder
    {
        private readonly KingdomHospitalContext _context;

        public OrdonnanceSeeder(KingdomHospitalContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Ordonnances.AnyAsync())
                return;

            var patients = await _context.Patients.OrderBy(p => p.PatientId).ToListAsync();
            var doctors = await _context.Doctors.OrderBy(d => d.DoctorId).ToListAsync();
            var medicaments = await _context.Medicaments.OrderBy(m => m.MedicamentId).ToListAsync();

            if (!patients.Any() || !doctors.Any() || medicaments.Count < 3)
                return;

            var ordonnances = new List<Ordonnance>();

            // Patient 1 a au moins 2 ordonnances
            var patientWithTwo = patients[0];
            var doc1 = doctors[0];

            // Ordonnance 1 pour le patient avec 2 ordonnances - contient 3 medicaments
            var o1 = new Ordonnance
            {
                DoctorId = doc1.DoctorId,
                PatientId = patientWithTwo.PatientId,
                ConsultationId = null,
                Date = DateOnly.FromDateTime(DateTime.Now.Date),
                Notes = "Traitement aigu",
                Doctor = doc1,
                Patient = patientWithTwo,
                OrdonnanceLignes = new List<OrdonnanceLigne>()
            };

            // ajoute 3 medicaments distinct
            o1.OrdonnanceLignes.Add(new OrdonnanceLigne
            {
                OrdonnanceId = o1.OrdonnanceId,
                MedicamentId = medicaments[0].MedicamentId,
                Dosage = medicaments[0].Strength,
                Frequency = "2 fois / jour",
                Duration = "7 jours",
                Quantity = 14,
                Instructions = "Après les repas",
                Ordonnance = o1,
                Medicament = medicaments[0]
            });
            o1.OrdonnanceLignes.Add(new OrdonnanceLigne
            {
                OrdonnanceId = o1.OrdonnanceId,
                MedicamentId = medicaments[1].MedicamentId,
                Dosage = medicaments[1].Strength,
                Frequency = "1 fois / jour",
                Duration = "10 jours",
                Quantity = 10,
                Instructions = null,
                Ordonnance = o1,
                Medicament = medicaments[1]
            });
            o1.OrdonnanceLignes.Add(new OrdonnanceLigne
            {
                OrdonnanceId = o1.OrdonnanceId,
                MedicamentId = medicaments[2].MedicamentId,
                Dosage = medicaments[2].Strength,
                Frequency = "1 fois / jour",
                Duration = "5 jours",
                Quantity = 5,
                Instructions = "Prendre le soir",
                Ordonnance = o1,
                Medicament = medicaments[2]
            });

            ordonnances.Add(o1);

            // Ordonnance 2 pour le même patient avec 1 medicament
            var o2 = new Ordonnance
            {
                DoctorId = doctors[1 % doctors.Count].DoctorId,
                PatientId = patientWithTwo.PatientId,
                ConsultationId = null,
                Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1)),
                Notes = "Suivi",
                Doctor = doctors[1 % doctors.Count],
                Patient = patientWithTwo,
                OrdonnanceLignes = new List<OrdonnanceLigne>()
            };

            o2.OrdonnanceLignes.Add(new OrdonnanceLigne
            {
                OrdonnanceId = o2.OrdonnanceId,
                MedicamentId = medicaments[0].MedicamentId,
                Dosage = medicaments[0].Strength,
                Frequency = "3 fois / jour",
                Duration = "5 jours",
                Quantity = 15,
                Instructions = null,
                Ordonnance = o2,
                Medicament = medicaments[0]
            });

            ordonnances.Add(o2);

            var otherPatients = patients.Skip(1).ToList();
            var medIndex = 0;
            foreach (var patient in otherPatients.Take(3))
            {
                var doc = doctors[(patient.PatientId) % doctors.Count];
                var ord = new Ordonnance
                {
                    DoctorId = doc.DoctorId,
                    PatientId = patient.PatientId,
                    ConsultationId = null,
                    Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2 + patient.PatientId)),
                    Notes = "Prescription standard",
                    Doctor = doc,
                    Patient = patient,
                    OrdonnanceLignes = new List<OrdonnanceLigne>()
                };

                ord.OrdonnanceLignes.Add(new OrdonnanceLigne
                {
                    OrdonnanceId = ord.OrdonnanceId,
                    MedicamentId = medicaments[medIndex % medicaments.Count].MedicamentId,
                    Dosage = "100 mg",
                    Frequency = "1 fois / jour",
                    Duration = "10 jours",
                    Quantity = 10,
                    Instructions = null,
                    Ordonnance = ord,
                    Medicament = medicaments[medIndex % medicaments.Count]
                });

                if (medIndex + 1 < medicaments.Count)
                {
                    ord.OrdonnanceLignes.Add(new OrdonnanceLigne
                    {
                        OrdonnanceId = ord.OrdonnanceId,
                        MedicamentId = medicaments[(medIndex + 1) % medicaments.Count].MedicamentId,
                        Dosage = "50 mg",
                        Frequency = "2 fois / jour",
                        Duration = "5 jours",
                        Quantity = 10,
                        Instructions = null,
                        Ordonnance = ord,
                        Medicament = medicaments[(medIndex + 1) % medicaments.Count]
                    });
                }

                medIndex++;
                ordonnances.Add(ord);
            }

            foreach (var o in ordonnances)
                _context.Ordonnances.Add(o);

            var allLignes = ordonnances
                .Where(o => o.OrdonnanceLignes != null)
                .SelectMany(o => o.OrdonnanceLignes!)
                .ToList();

            foreach (var l in allLignes)
                _context.OrdonnanceLignes.Add(l);

            await _context.SaveChangesAsync();
        }
    }
}