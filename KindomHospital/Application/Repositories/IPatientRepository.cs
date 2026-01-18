using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<int> AddPatientAsync(Patient patient);

        Task<Patient> GetPatientById(int id);
    }
}