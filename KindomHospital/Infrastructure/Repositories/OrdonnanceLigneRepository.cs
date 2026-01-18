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

        public async Task<int> AddOrdonnanceLigneAsync(OrdonnanceLigne ordonnanceLigne)
        {
            var ordonnanceExists = await context.Ordonnances.AnyAsync(o => o.OrdonnanceId == ordonnanceLigne.OrdonnanceId);
            var medicamentExists = await context.Medicaments.AnyAsync(m => m.MedicamentId == ordonnanceLigne.MedicamentId);

            if (!ordonnanceExists || !medicamentExists)
                return -1;

            await context.OrdonnanceLignes.AddAsync(ordonnanceLigne);
            await context.SaveChangesAsync();
            return ordonnanceLigne.OrdonnanceLigneId;
        }

        public async Task<int> AddOrdonnanceLignesAsync(IEnumerable<OrdonnanceLigne> ordonnanceLignes)
        {
            foreach (var line in ordonnanceLignes)
            {
                var ordonnanceExists = await context.Ordonnances.AnyAsync(o => o.OrdonnanceId == line.OrdonnanceId);
                var medicamentExists = await context.Medicaments.AnyAsync(m => m.MedicamentId == line.MedicamentId);
                if (!ordonnanceExists || !medicamentExists)
                    return -1;
            }

            await context.OrdonnanceLignes.AddRangeAsync(ordonnanceLignes);
            await context.SaveChangesAsync();
            return ordonnanceLignes.Count();
        }

        public async Task<IEnumerable<OrdonnanceLigne>> GetLignesByOrdonnanceAsync(int ordonnanceId)
        {
            return await context.OrdonnanceLignes
                .Where(ol => ol.OrdonnanceId == ordonnanceId)
                .ToListAsync();
        }

        public async Task<OrdonnanceLigne?> GetOrdonnanceLigneByIdAsync(int ligneId)
        {
            return await context.OrdonnanceLignes
                .FirstOrDefaultAsync(ol => ol.OrdonnanceLigneId == ligneId);
        }

        public async Task<int> UpdateOrdonnanceLigneAsync(OrdonnanceLigne ordonnanceLigne)
        {
            var ordExists = await context.Ordonnances.AnyAsync(o => o.OrdonnanceId == ordonnanceLigne.OrdonnanceId);
            var medExists = await context.Medicaments.AnyAsync(m => m.MedicamentId == ordonnanceLigne.MedicamentId);

            if (!ordExists || !medExists)
                return -1;

            var existing = await context.OrdonnanceLignes.FindAsync(ordonnanceLigne.OrdonnanceLigneId);
            if (existing is null)
                return 0;

            existing.MedicamentId = ordonnanceLigne.MedicamentId;
            existing.Dosage = ordonnanceLigne.Dosage;
            existing.Frequency = ordonnanceLigne.Frequency;
            existing.Duration = ordonnanceLigne.Duration;
            existing.Quantity = ordonnanceLigne.Quantity;
            existing.Instructions = ordonnanceLigne.Instructions;

            await context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteOrdonnanceLigneAsync(int ligneId)
        {
            var existing = await context.OrdonnanceLignes.FindAsync(ligneId);
            if (existing is null)
                return 0;

            context.OrdonnanceLignes.Remove(existing);
            await context.SaveChangesAsync();
            return 1;
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