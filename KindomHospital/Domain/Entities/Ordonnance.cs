using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Ordonnance
    {
        
        public int OrdonnanceId { get; set; }

        
        public  int DoctorId { get; set; }

        
        public  int PatientId { get; set; }

        public int? ConsultationId { get; set; }

        
        public  DateOnly Date { get; set; }

        
        public string? Notes { get; set; }

        
        public Doctor? Doctor { get; set; }

        
        public Patient? Patient { get; set; }

        
        public Consultation? Consultation { get; set; }

        public ICollection<OrdonnanceLigne>? OrdonnanceLignes { get; set; }
    }
}