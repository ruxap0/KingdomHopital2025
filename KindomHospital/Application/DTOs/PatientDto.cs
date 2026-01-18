using System;

namespace KindomHospital.Application.DTOs
{
    public record PatientDto(int PatientId, string FirstName, string LastName, DateOnly BirthDate);

    public record CreatePatientDto(string FirstName, string LastName, DateOnly BirthDate);
}