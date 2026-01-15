using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Domain.Entities
{
    
    public class Medicament
    {
        
        public int MedicamentId { get; set; }

        
        
        public required string Name { get; set; }

        
        
        public required string DosageForm { get; set; }

        
        
        public required string Strength { get; set; }

        
        public string? AtcCode { get; set; }

        public ICollection<OrdonnanceLigne>? OrdonnanceLignes { get; set; }
    }
}