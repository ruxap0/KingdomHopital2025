using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class MedicamentService(IMedicamentRepository medicamentRepository, MedicamentMapper medicamentMapper, OrdonnanceMapper ordonnanceMapper, ILogger<MedicamentService> logger)
    {
        public async Task<IEnumerable<MedicamentDto>> GetAllMedicamentsAsync()
        {
            logger.LogInformation("GetAllMedicamentsAsync");
            var entities = await medicamentRepository.GetAllMedicamentsAsync();
            var dtos = entities.Select(medicamentMapper.ToDto);
            return dtos;
        }

        public async Task<MedicamentDto> GetMedicamentById(int id)
        {
            logger.LogInformation("GetMedicamentById; id : " + id);
            var entity = await medicamentRepository.GetMedicamentById(id);
            MedicamentDto dto = entity is null ? null : medicamentMapper.ToDto(entity);
            return dto;
        }

        public async Task<int> Add(CreateMedicamentDto dto)
        {
            logger.LogInformation("Add Medicament");
            var entity = medicamentMapper.ToEntity(dto);
            return await medicamentRepository.AddMedicamentAsync(entity);
        }

        public async Task<IEnumerable<OrdonnanceDto>?> GetOrdonnancesByMedicamentAsync(int medicamentId)
        {
            logger.LogInformation("GetOrdonnancesByMedicamentAsync; medicamentId : " + medicamentId);
            var med = await medicamentRepository.GetMedicamentById(medicamentId);
            if (med is null)
                return null;

            var ords = await medicamentRepository.GetOrdonnancesByMedicamentAsync(medicamentId);
            return ords.Select(ordonnanceMapper.ToDto);
        }
    }
}