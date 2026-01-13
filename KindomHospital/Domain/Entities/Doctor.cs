using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        public required int SpecialtyId { get; set; }

        [Required]
        [MaxLength(30)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public required string LastName { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public required Specialty Specialty { get; set; }

        public ICollection<Consultation>? Consultations { get; set; }

        public ICollection<Ordonnance>? Ordonnances { get; set; }
    }
}
