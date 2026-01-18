using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class PatientService(IPatientRepository patientRepository, PatientMapper patientMapper, ILogger<PatientService> logger)
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
    }
}