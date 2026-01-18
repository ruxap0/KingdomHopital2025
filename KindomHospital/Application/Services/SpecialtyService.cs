using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class SpecialtyService(ISpecialtyRepository specialtyRepository, SpecialtyMapper specialtyMapper, IDoctorRepository doctorRepository, DoctorMapper doctorMapper, ILogger<SpecialtyService> logger)
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

        public async Task<IEnumerable<DoctorDto>?> GetDoctorsBySpecialtyAsync(int specialtyId)
        {
            logger.LogInformation("GetDoctorsBySpecialtyAsync; specialtyId : " + specialtyId);

            var specialty = await specialtyRepository.GetSpecialtyById(specialtyId);
            if (specialty is null)
                return null;

            var doctors = await doctorRepository.GetDoctorsBySpecialtyIdAsync(specialtyId);
            return doctors.Select(doctorMapper.ToDto);
        }
    }
}
