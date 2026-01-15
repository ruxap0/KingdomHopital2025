using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class OrdonnanceLigne
    {
        
        public int OrdonnanceLigneId { get; set; }

        
        public required int OrdonnanceId { get; set; }

        
        public required int MedicamentId { get; set; }

        
        
        public required string Dosage { get; set; }

        
        
        public required string Frequency { get; set; }

        
        
        public required string Duration { get; set; }

        
        
        public required int Quantity { get; set; }

        
        public string? Instructions { get; set; }

        
        public required Ordonnance Ordonnance { get; set; }

        
        public required Medicament Medicament { get; set; }
    }
}