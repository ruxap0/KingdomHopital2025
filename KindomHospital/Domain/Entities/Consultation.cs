using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Consultation
    {
        [Key]
        public int ConsultationId { get; set; }

        [Required]
        public required int DoctorId { get; set; }

        [Required]
        public required int PatientId { get; set; }

        [Required]
        public required DateOnly Date { get; set; }

        [Required]
        public required TimeOnly Hour { get; set; }

        [MaxLength(100)]
        public string? Reason { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public required Doctor Doctor { get; set; }

        [ForeignKey(nameof(PatientId))]
        public required Patient Patient { get; set; }

        public ICollection<Ordonnance>? Ordonnances { get; set; }
    }
}