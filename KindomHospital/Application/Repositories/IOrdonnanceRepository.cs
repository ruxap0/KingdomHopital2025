using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IOrdonnanceRepository
    {
        Task<IEnumerable<Ordonnance>> GetAllOrdonnancesAsync();

        Task<int> AddOrdonnanceAsync(Ordonnance ordonnance);

        Task<Ordonnance> GetOrdonnanceById(int id);

        Task<int> UpdateOrdonnanceAsync(Ordonnance ordonnance);

        Task<int> DeleteOrdonnanceAsync(int id);

        Task<IEnumerable<Ordonnance>> GetOrdonnancesByConsultationAsync(int consultationId);

        Task<int> AttachOrdonnanceToConsultationAsync(int ordonnanceId, int consultationId);

        Task<int> DetachOrdonnanceFromConsultationAsync(int ordonnanceId);

        Task<IEnumerable<Ordonnance>> GetOrdonnancesFilteredAsync(int? doctorId, int? patientId, DateOnly? from, DateOnly? to);
    }
}