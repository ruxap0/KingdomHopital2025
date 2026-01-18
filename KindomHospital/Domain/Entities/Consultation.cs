using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Consultation
    {
        
        public int ConsultationId { get; set; }

        
        public int DoctorId { get; set; }

        
        public int PatientId { get; set; }

        
        public DateOnly Date { get; set; }

        
        public TimeOnly Hour { get; set; }

        
        public string? Reason { get; set; }

        
        public Doctor? Doctor { get; set; }

        
        public Patient? Patient { get; set; }

        public ICollection<Ordonnance>? Ordonnances { get; set; }
    }
}