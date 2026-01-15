using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Domain.Entities
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }

        public required string Name { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }
    }
}
