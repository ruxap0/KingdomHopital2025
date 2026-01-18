namespace KindomHospital.Application.DTOs
{
    public record MedicamentDto(int MedicamentId, string Name, string DosageForm, string Strength, string? AtcCode);
}