using System;

namespace KindomHospital.Application.DTOs
{
    public record ConsultationDto(int ConsultationId, int DoctorId, int PatientId, DateOnly Date, TimeOnly Hour, string? Reason);
}