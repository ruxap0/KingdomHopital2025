using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Consultation
    {
        
        public int ConsultationId { get; set; }

        
        public required int DoctorId { get; set; }

        
        public required int PatientId { get; set; }

        
        public required DateOnly Date { get; set; }

        
        public required TimeOnly Hour { get; set; }

        
        public string? Reason { get; set; }

        
        public required Doctor Doctor { get; set; }

        
        public required Patient Patient { get; set; }

        public ICollection<Ordonnance>? Ordonnances { get; set; }
    }
}