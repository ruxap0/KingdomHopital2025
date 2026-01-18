using System;

namespace KindomHospital.Application.DTOs
{
    public record OrdonnanceDto(int OrdonnanceId, int DoctorId, int PatientId, int? ConsultationId, DateOnly Date, string? Notes);
}