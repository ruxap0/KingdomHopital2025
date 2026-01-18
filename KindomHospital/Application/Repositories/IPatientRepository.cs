using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<int> AddPatientAsync(Patient patient);

        Task<Patient> GetPatientById(int id);

        Task<int> UpdatePatientAsync(Patient patient);

        Task<int> DeletePatientAsync(int id);

        Task<IEnumerable<Consultation>> GetConsultationsByPatientAsync(int patientId);

        Task<IEnumerable<Ordonnance>> GetOrdonnancesByPatientAsync(int patientId);
    }
}