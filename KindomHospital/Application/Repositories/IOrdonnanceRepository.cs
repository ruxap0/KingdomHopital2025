using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IOrdonnanceRepository
    {
        Task<IEnumerable<Ordonnance>> GetAllOrdonnancesAsync();

        Task<int> AddOrdonnanceAsync(Ordonnance ordonnance);

        Task<Ordonnance> GetOrdonnanceById(int id);

        Task<int> UpdateOrdonnanceAsync(Ordonnance ordonnance);
    }
}