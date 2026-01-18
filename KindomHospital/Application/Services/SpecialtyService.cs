using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class SpecialtyService(ISpecialtyRepository specialtyRepository, SpecialtyMapper specialtyMapper, ILogger<SpecialtyService> logger)
    {
        public async Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync()
        {
            logger.LogInformation("GetAllSpecialtiesAsync");
            var entities = await specialtyRepository.GetAllSpecialtiesAsync();
            var dtos = entities.Select(specialtyMapper.ToDto);

            return dtos;
        }

        public async Task<SpecialtyDto> GetSpecialtyById(int id)
        {
            logger.LogInformation("GetSpecialtyById; id : " + id);
            var entity = await specialtyRepository.GetSpecialtyById(id);
            SpecialtyDto dto;
            if (entity != null)
            {
                dto = specialtyMapper.ToDto(entity);
            }
            else
            {
                dto = null;
            }
                return dto;
        }
    }
}
