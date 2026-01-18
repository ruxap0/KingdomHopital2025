using KindomHospital.Domain.Entities;

namespace KindomHospital.Application.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        Task<int> AddDoctorAsync(Doctor doctor);

        Task<Doctor> GetDoctorById(int id);

        Task<int> UpdateDoctorAsync(Doctor doctor);

        Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyIdAsync(int specialtyId);

        Task<Specialty?> GetSpecialtyByDoctorIdAsync(int doctorId);

        Task<int> ChangeDoctorSpecialtyAsync(int doctorId, int specialtyId);

        Task<IEnumerable<Consultation>> GetConsultationsByDoctorAsync(int doctorId, DateOnly? from, DateOnly? to);

        Task<IEnumerable<Patient>> GetPatientsByDoctorAsync(int doctorId);

        Task<IEnumerable<Ordonnance>> GetOrdonnancesByDoctorAsync(int doctorId, DateOnly? from, DateOnly? to);
    }
}