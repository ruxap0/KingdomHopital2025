using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IMedicamentRepository
    {
        Task<IEnumerable<Medicament>> GetAllMedicamentsAsync();

        Task<int> AddMedicamentAsync(Medicament medicament);

        Task<Medicament> GetMedicamentById(int id);

        Task<IEnumerable<Ordonnance>> GetOrdonnancesByMedicamentAsync(int medicamentId);
    }
}