using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class DoctorService(IDoctorRepository doctorRepository, DoctorMapper doctorMapper, ConsultationMapper consultationMapper, PatientMapper patientMapper, OrdonnanceMapper ordonnanceMapper, SpecialtyMapper specialtyMapper, ILogger<DoctorService> logger)
    {
        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            logger.LogInformation("GetAllDoctorsAsync");
            var entities = await doctorRepository.GetAllDoctorsAsync();
            var dtos = entities.Select(doctorMapper.ToDto);
            return dtos;
        }

        public async Task<DoctorDto> GetDoctorById(int id)
        {
            logger.LogInformation("GetDoctorById; id : " + id);
            var entity = await doctorRepository.GetDoctorById(id);
            DoctorDto dto;
            if (entity != null)
            {
                dto = doctorMapper.ToDto(entity);
            }
            else
            {
                dto = null;
            }
            return dto;
        }

        public async Task<int> Add(CreateDoctorDto dto)
        {
            logger.LogInformation("Add Doctor");
            var entity = doctorMapper.ToEntity(dto);
            return await doctorRepository.AddDoctorAsync(entity);
        }

        public async Task<int> Update(int id, CreateDoctorDto dto)
        {
            logger.LogInformation("Update Doctor; id : " + id);
            var entity = doctorMapper.ToEntity(dto);
            entity.DoctorId = id;
            return await doctorRepository.UpdateDoctorAsync(entity);
        }

        public async Task<IEnumerable<ConsultationDto>?> GetConsultationsByDoctorAsync(int doctorId, DateOnly? from, DateOnly? to)
        {
            logger.LogInformation("GetConsultationsByDoctorAsync; doctorId : " + doctorId);
            var doctor = await doctorRepository.GetDoctorById(doctorId);
            if (doctor is null)
                return null;

            var consultations = await doctorRepository.GetConsultationsByDoctorAsync(doctorId, from, to);
            return consultations.Select(consultationMapper.ToDto);
        }

        public async Task<IEnumerable<PatientDto>?> GetPatientsByDoctorAsync(int doctorId)
        {
            logger.LogInformation("GetPatientsByDoctorAsync; doctorId : " + doctorId);
            var doctor = await doctorRepository.GetDoctorById(doctorId);
            if (doctor is null)
                return null;

            var patients = await doctorRepository.GetPatientsByDoctorAsync(doctorId);
            return patients.Select(patientMapper.ToDto);
        }

        public async Task<IEnumerable<OrdonnanceDto>?> GetOrdonnancesByDoctorAsync(int doctorId, DateOnly? from, DateOnly? to)
        {
            logger.LogInformation("GetOrdonnancesByDoctorAsync; doctorId : " + doctorId);
            var doctor = await doctorRepository.GetDoctorById(doctorId);
            if (doctor is null)
                return null;

            var ordonnances = await doctorRepository.GetOrdonnancesByDoctorAsync(doctorId, from, to);
            return ordonnances.Select(ordonnanceMapper.ToDto);
        }

        public async Task<SpecialtyDto?> GetSpecialtyByDoctorAsync(int doctorId)
        {
            logger.LogInformation("GetSpecialtyByDoctorAsync; doctorId : " + doctorId);
            var specialty = await doctorRepository.GetSpecialtyByDoctorIdAsync(doctorId);
            if (specialty is null)
                return null;
            return specialtyMapper.ToDto(specialty);
        }

        public async Task<int> ChangeSpecialty(int doctorId, int specialtyId)
        {
            logger.LogInformation("ChangeSpecialty; doctorId : " + doctorId + " specialtyId : " + specialtyId);
            return await doctorRepository.ChangeDoctorSpecialtyAsync(doctorId, specialtyId);
        }
    }
}