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
    }
}