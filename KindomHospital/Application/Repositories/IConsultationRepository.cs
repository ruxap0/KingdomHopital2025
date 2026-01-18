using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IConsultationRepository
    {
        Task<IEnumerable<Consultation>> GetAllConsultationsAsync();

        Task<int> AddConsultationAsync(Consultation consultation);

        Task<Consultation> GetConsultationById(int id);

        Task<int> UpdateConsultationAsync(Consultation consultation);
    }
}