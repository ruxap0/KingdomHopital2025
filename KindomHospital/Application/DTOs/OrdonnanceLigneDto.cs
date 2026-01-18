namespace KindomHospital.Application.DTOs
{
    public record OrdonnanceLigneDto(int OrdonnanceLigneId, int OrdonnanceId, int MedicamentId, string Dosage, string Frequency, string Duration, int Quantity, string? Instructions);

    public record CreateOrdonnanceLigneDto(int MedicamentId, string Dosage, string Frequency, string Duration, int Quantity, string? Instructions);
}