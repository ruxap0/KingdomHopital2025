using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync();

        Task AddSpecialtyAsync(Specialty specialty);

        Task<Specialty> GetSpecialtyById(int id);
    }
}
