using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Doctor
    {
        
        public int DoctorId { get; set; }

        public required int SpecialtyId { get; set; }
        
        public required string FirstName { get; set; }

        public required string LastName { get; set; }
        
        public required Specialty Specialty { get; set; }

        public ICollection<Consultation>? Consultations { get; set; }

        public ICollection<Ordonnance>? Ordonnances { get; set; }
    }
}
