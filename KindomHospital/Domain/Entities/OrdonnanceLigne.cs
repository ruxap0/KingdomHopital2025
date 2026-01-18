using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KindomHospital.Domain.Entities
{
    public class OrdonnanceLigne
    {
        
        public int OrdonnanceLigneId { get; set; }

        
        public  int OrdonnanceId { get; set; }

        
        public  int MedicamentId { get; set; }

        
        
        public  string Dosage { get; set; }

        
        
        public  string Frequency { get; set; }

        
        
        public  string Duration { get; set; }

        
        
        public  int Quantity { get; set; }

        
        public string? Instructions { get; set; }

        
        public  Ordonnance? Ordonnance { get; set; }

        
        public  Medicament? Medicament { get; set; }
    }
}