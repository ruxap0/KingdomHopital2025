using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IMedicamentRepository
    {
        Task<IEnumerable<Medicament>> GetAllMedicamentsAsync();

        Task AddMedicamentAsync(Medicament medicament);
    }
}