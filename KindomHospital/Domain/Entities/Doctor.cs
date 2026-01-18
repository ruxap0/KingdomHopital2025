using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Doctor
    {
        
        public int DoctorId { get; set; }

        public int SpecialtyId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public Specialty? Specialty { get; set; }

        public ICollection<Consultation>? Consultations { get; set; }

        public ICollection<Ordonnance>? Ordonnances { get; set; }
    }
}
