using KindomHospital.Infrastructure.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Seeders
{
    public class SpecialtySeeder : ISeeder
    {
        private readonly DbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public SpecialtySeeder(DbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public async Task Seed()
        {
            if (await _dbContext.Specialties.AnyAsync())
            {
                return;
            }

            return Task.CompletedTask;
        }
    }
}
