using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IOrdonnanceLigneRepository
    {
        Task<IEnumerable<OrdonnanceLigne>> GetAllOrdonnanceLignesAsync();

        Task<int> AddOrdonnanceLigneAsync(OrdonnanceLigne ordonnanceLigne);

        Task<int> AddOrdonnanceLignesAsync(IEnumerable<OrdonnanceLigne> ordonnanceLignes);

        Task<IEnumerable<OrdonnanceLigne>> GetLignesByOrdonnanceAsync(int ordonnanceId);

        Task<OrdonnanceLigne?> GetOrdonnanceLigneByIdAsync(int ligneId);

        Task<int> UpdateOrdonnanceLigneAsync(OrdonnanceLigne ordonnanceLigne);

        Task<int> DeleteOrdonnanceLigneAsync(int ligneId);

        Task<IEnumerable<KindomHospital.Domain.Entities.Ordonnance>> GetOrdonnancesByMedicamentAsync(int medicamentId);
    }
}