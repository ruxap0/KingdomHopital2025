using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

// Fait
namespace KindomHospital.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Specialty
    {
        [Key]
        public int SpecialtyId { get; set; }

        
        [MaxLength(30)]
        [Required]
        public required string Name { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }
    }
}
