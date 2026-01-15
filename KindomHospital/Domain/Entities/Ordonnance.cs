using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Ordonnance
    {
        
        public int OrdonnanceId { get; set; }

        
        public required int DoctorId { get; set; }

        
        public required int PatientId { get; set; }

        public int? ConsultationId { get; set; }

        
        public required DateOnly Date { get; set; }

        
        public string? Notes { get; set; }

        
        public required Doctor Doctor { get; set; }

        
        public required Patient Patient { get; set; }

        
        public Consultation? Consultation { get; set; }

        public ICollection<OrdonnanceLigne>? OrdonnanceLignes { get; set; }
    }
}