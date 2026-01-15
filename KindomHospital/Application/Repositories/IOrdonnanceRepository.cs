using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IOrdonnanceRepository
    {
        Task<IEnumerable<Ordonnance>> GetAllOrdonnancesAsync();

        Task AddOrdonnanceAsync(Ordonnance ordonnance);
    }
}