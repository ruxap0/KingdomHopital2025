using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class PatientService(IPatientRepository patientRepository, PatientMapper patientMapper, ConsultationMapper consultationMapper, OrdonnanceMapper ordonnanceMapper, ILogger<PatientService> logger)
    {
        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            logger.LogInformation("GetAllPatientsAsync");
            var entities = await patientRepository.GetAllPatientsAsync();
            var dtos = entities.Select(patientMapper.ToDto);
            return dtos;
        }

        public async Task<PatientDto> GetPatientById(int id)
        {
            logger.LogInformation("GetPatientById; id : " + id);
            var entity = await patientRepository.GetPatientById(id);
            PatientDto dto;
            if (entity != null)
            {
                dto = patientMapper.ToDto(entity);
            }
            else
            {
                dto = null;
            }
            return dto;
        }

        public async Task<int> Add(CreatePatientDto dto)
        {
            logger.LogInformation("Add Patient");
            var entity = patientMapper.ToEntity(dto);
            return await patientRepository.AddPatientAsync(entity);
        }

        public async Task<int> Update(int id, CreatePatientDto dto)
        {
            logger.LogInformation("Update Patient; id : " + id);
            var entity = patientMapper.ToEntity(dto);
            entity.PatientId = id;
            return await patientRepository.UpdatePatientAsync(entity);
        }

        public async Task<int> Delete(int id)
        {
            logger.LogInformation("Delete Patient; id : " + id);
            return await patientRepository.DeletePatientAsync(id);
        }

        public async Task<IEnumerable<ConsultationDto>?> GetConsultationsByPatientAsync(int patientId)
        {
            logger.LogInformation("GetConsultationsByPatientAsync; patientId : " + patientId);
            var patient = await patientRepository.GetPatientById(patientId);
            if (patient is null)
                return null;

            var consultations = await patientRepository.GetConsultationsByPatientAsync(patientId);
            return consultations.Select(consultationMapper.ToDto);
        }

        public async Task<IEnumerable<OrdonnanceDto>?> GetOrdonnancesByPatientAsync(int patientId)
        {
            logger.LogInformation("GetOrdonnancesByPatientAsync; patientId : " + patientId);
            var patient = await patientRepository.GetPatientById(patientId);
            if (patient is null)
                return null;

            var ordonnances = await patientRepository.GetOrdonnancesByPatientAsync(patientId);
            return ordonnances.Select(ordonnanceMapper.ToDto);
        }
    }
}