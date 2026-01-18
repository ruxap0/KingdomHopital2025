using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class ConsultationService(IConsultationRepository consultationRepository, ConsultationMapper consultationMapper, IDoctorRepository doctorRepository, IPatientRepository patientRepository, ILogger<ConsultationService> logger)
    {
        public async Task<IEnumerable<ConsultationDto>> GetAllConsultationsAsync()
        {
            logger.LogInformation("GetAllConsultationsAsync");
            var entities = await consultationRepository.GetAllConsultationsAsync();
            var dtos = entities.Select(consultationMapper.ToDto);
            return dtos;
        }

        public async Task<ConsultationDto> GetConsultationById(int id)
        {
            logger.LogInformation("GetConsultationById; id : " + id);
            var entity = await consultationRepository.GetConsultationById(id);
            ConsultationDto dto;
            if (entity != null)
            {
                dto = consultationMapper.ToDto(entity);
            }
            else
            {
                dto = null;
            }
            return dto;
        }

        public async Task<int> Add(CreateConsultationDto dto)
        {
            logger.LogInformation("Add Consultation");
            var entity = consultationMapper.ToEntity(dto);
            return await consultationRepository.AddConsultationAsync(entity);
        }

        public async Task<int> Update(int id, CreateConsultationDto dto)
        {
            logger.LogInformation("Update Consultation; id : " + id);
            var entity = consultationMapper.ToEntity(dto);
            entity.ConsultationId = id;
            return await consultationRepository.UpdateConsultationAsync(entity);
        }

        public async Task<IEnumerable<ConsultationDto>?> GetFilteredConsultationsAsync(int? doctorId, int? patientId, DateOnly? from, DateOnly? to)
        {
            logger.LogInformation("GetFilteredConsultationsAsync; doctorId: {DoctorId}, patientId: {PatientId}, from: {From}, to: {To}", doctorId, patientId, from, to);

            if (doctorId.HasValue)
            {
                var doc = await doctorRepository.GetDoctorById(doctorId.Value);
                if (doc is null)
                    return null;
            }

            if (patientId.HasValue)
            {
                var pat = await patientRepository.GetPatientById(patientId.Value);
                if (pat is null)
                    return null;
            }

            var entities = await consultationRepository.GetConsultationsFilteredAsync(doctorId, patientId, from, to);
            return entities.Select(consultationMapper.ToDto);
        }
    }
}