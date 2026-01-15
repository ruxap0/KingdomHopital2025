using KindomHospital.Domain.Entities;
using KindomHospital.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class SpecialtyRepository(KingdomHospitalContext context) : ISpecialtyRepository
    {
        public async Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync()
        {
            return await context.Specialties.ToListAsync();
        }
        public async Task AddSpecialtyAsync(Specialty specialty)
        {
            await context.Specialties.AddAsync(specialty);
            await context.SaveChangesAsync();
        }
    }
}
