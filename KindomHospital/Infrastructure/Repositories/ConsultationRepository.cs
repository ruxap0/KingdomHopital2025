using KindomHospital.Application.Repositories;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Repositories
{
    public class ConsultationRepository(KingdomHospitalContext context) : IConsultationRepository
    {
        public async Task<IEnumerable<Consultation>> GetAllConsultationsAsync()
        {
            return await context.Consultations.ToListAsync();
        }

        public async Task AddConsultationAsync(Consultation consultation)
        {
            await context.Consultations.AddAsync(consultation);
            await context.SaveChangesAsync();
        }

        public async Task<Consultation> GetConsultationById(int id)
        {
            return await context.Consultations.FirstOrDefaultAsync(c => c.ConsultationId == id);
        }
    }
}