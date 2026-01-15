using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IOrdonnanceLigneRepository
    {
        Task<IEnumerable<OrdonnanceLigne>> GetAllOrdonnanceLignesAsync();

        Task AddOrdonnanceLigneAsync(OrdonnanceLigne ordonnanceLigne);
    }
}