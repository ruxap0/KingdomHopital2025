using KindomHospital.Application.Repositories;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class MedicamentRepository(KingdomHospitalContext context) : IMedicamentRepository
    {
        public async Task<IEnumerable<Medicament>> GetAllMedicamentsAsync()
        {
            return await context.Medicaments.ToListAsync();
        }

        public async Task<int> AddMedicamentAsync(Medicament medicament)
        {
            await context.Medicaments.AddAsync(medicament);
            await context.SaveChangesAsync();
            return medicament.MedicamentId;
        }

        public async Task<Medicament> GetMedicamentById(int id)
        {
            return await context.Medicaments.FirstOrDefaultAsync(m => m.MedicamentId == id);
        }

        public async Task<IEnumerable<Ordonnance>> GetOrdonnancesByMedicamentAsync(int medicamentId)
        {
            var ordonnanceIds = await context.OrdonnanceLignes
                .Where(ol => ol.MedicamentId == medicamentId)
                .Select(ol => ol.OrdonnanceId)
                .Distinct()
                .ToListAsync();

            if (!ordonnanceIds.Any())
                return Array.Empty<Ordonnance>();

            return await context.Ordonnances
                .Where(o => ordonnanceIds.Contains(o.OrdonnanceId))
                .OrderBy(o => o.Date)
                .ToListAsync();
        }
    }
}