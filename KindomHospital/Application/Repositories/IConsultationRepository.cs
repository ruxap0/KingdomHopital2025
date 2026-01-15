using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IConsultationRepository
    {
        Task<IEnumerable<Consultation>> GetAllConsultationsAsync();

        Task AddConsultationAsync(Consultation consultation);
    }
}