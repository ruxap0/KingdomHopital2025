using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class OrdonnanceLigne
    {
        [Key]
        public int OrdonnanceLigneId { get; set; }

        [Required]
        public required int OrdonnanceId { get; set; }

        [Required]
        public required int MedicamentId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Dosage { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Frequency { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Duration { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public required int Quantity { get; set; }

        [MaxLength(255)]
        public string? Instructions { get; set; }

        [ForeignKey(nameof(OrdonnanceId))]
        public required Ordonnance Ordonnance { get; set; }

        [ForeignKey(nameof(MedicamentId))]
        public required Medicament Medicament { get; set; }
    }
}