using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Medicament
    {
        [Key]
        public int MedicamentId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public required string DosageForm { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Strength { get; set; }

        [MaxLength(20)]
        public string? AtcCode { get; set; }

        public ICollection<OrdonnanceLigne>? OrdonnanceLignes { get; set; }
    }
}