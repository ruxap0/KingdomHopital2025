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

        public async Task AddMedicamentAsync(Medicament medicament)
        {
            await context.Medicaments.AddAsync(medicament);
            await context.SaveChangesAsync();
        }
    }
}