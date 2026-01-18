using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class OrdonnanceService(IOrdonnanceRepository ordonnanceRepository, OrdonnanceMapper ordonnanceMapper, ILogger<OrdonnanceService> logger)
    {
        public async Task<IEnumerable<OrdonnanceDto>> GetAllOrdonnancesAsync()
        {
            logger.LogInformation("GetAllOrdonnancesAsync");
            var entities = await ordonnanceRepository.GetAllOrdonnancesAsync();
            var dtos = entities.Select(ordonnanceMapper.ToDto);
            return dtos;
        }

        public async Task<OrdonnanceDto> GetOrdonnanceById(int id)
        {
            logger.LogInformation("GetOrdonnanceById; id : " + id);
            var entity = await ordonnanceRepository.GetOrdonnanceById(id);
            OrdonnanceDto dto;
            if (entity != null)
            {
                dto = ordonnanceMapper.ToDto(entity);
            }
            else
            {
                dto = null;
            }
            return dto;
        }

        public async Task<int> Add(CreateOrdonnanceDto dto)
        {
            logger.LogInformation("Add Ordonnance");
            var entity = ordonnanceMapper.ToEntity(dto);
            return await ordonnanceRepository.AddOrdonnanceAsync(entity);
        }
    }
}