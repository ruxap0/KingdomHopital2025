using System.ComponentModel.DataAnnotations;

namespace KindomHospital.Domain.Entities
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [MaxLength(30)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public required string LastName { get; set; }

        [Required]
        public required DateOnly BirthDate { get; set; }

        public ICollection<Consultation>? Consultations { get; set; }
        public ICollection<Ordonnance>? Ordonnances { get; set; }
    }
}