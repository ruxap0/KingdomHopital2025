namespace KindomHospital.Application.DTOs
{
    public record DoctorDto(int DoctorId, int SpecialtyId, string FirstName, string LastName);

    public record CreateDoctorDto(int SpecialtyId, string FirstName, string LastName);
}