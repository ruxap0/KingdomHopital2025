using KindomHospital.Application.Repositories;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class OrdonnanceRepository(KingdomHospitalContext context) : IOrdonnanceRepository
    {
        public async Task<IEnumerable<Ordonnance>> GetAllOrdonnancesAsync()
        {
            return await context.Ordonnances.ToListAsync();
        }

        public async Task AddOrdonnanceAsync(Ordonnance ordonnance)
        {
            await context.Ordonnances.AddAsync(ordonnance);
            await context.SaveChangesAsync();
        }

        public async Task<Ordonnance> GetOrdonnanceById(int id)
        {
            return await context.Ordonnances.FirstOrDefaultAsync(o => o.OrdonnanceId == id);
        }
    }
}