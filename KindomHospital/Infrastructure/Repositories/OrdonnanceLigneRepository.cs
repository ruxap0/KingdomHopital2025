using KindomHospital.Application.Repositories;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class OrdonnanceLigneRepository(KingdomHospitalContext context) : IOrdonnanceLigneRepository
    {
        public async Task<IEnumerable<OrdonnanceLigne>> GetAllOrdonnanceLignesAsync()
        {
            return await context.OrdonnanceLignes.ToListAsync();
        }

        public async Task AddOrdonnanceLigneAsync(OrdonnanceLigne ordonnanceLigne)
        {
            await context.OrdonnanceLignes.AddAsync(ordonnanceLigne);
            await context.SaveChangesAsync();
        }
    }
}