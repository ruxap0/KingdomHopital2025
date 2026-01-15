using System.ComponentModel.DataAnnotations;

namespace KindomHospital.Domain.Entities
{
    public class Patient
    {
        
        public int PatientId { get; set; }

        
        
        public required string FirstName { get; set; }

        
        
        public required string LastName { get; set; }

        
        public required DateOnly BirthDate { get; set; }

        public ICollection<Consultation>? Consultations { get; set; }
        public ICollection<Ordonnance>? Ordonnances { get; set; }
    }
}