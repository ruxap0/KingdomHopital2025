using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class ConsultationService(IConsultationRepository consultationRepository, ConsultationMapper consultationMapper, ILogger<ConsultationService> logger)
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
    }
}