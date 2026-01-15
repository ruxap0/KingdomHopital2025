using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task AddPatientAsync(Patient patient);
    }
}