using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        Task<int> AddDoctorAsync(Doctor doctor);

        Task<Doctor> GetDoctorById(int id);
    }
}