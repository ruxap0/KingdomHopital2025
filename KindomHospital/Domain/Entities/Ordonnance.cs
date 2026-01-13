using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class Ordonnance
    {
        [Key]
        public int OrdonnanceId { get; set; }

        [Required]
        public required int DoctorId { get; set; }

        [Required]
        public required int PatientId { get; set; }

        public int? ConsultationId { get; set; }

        [Required]
        public required DateOnly Date { get; set; }

        [MaxLength(255)]
        public string? Notes { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public required Doctor Doctor { get; set; }

        [ForeignKey(nameof(PatientId))]
        public required Patient Patient { get; set; }

        [ForeignKey(nameof(ConsultationId))]
        public Consultation? Consultation { get; set; }

        public ICollection<OrdonnanceLigne>? OrdonnanceLignes { get; set; }
    }
}