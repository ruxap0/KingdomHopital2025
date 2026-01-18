using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Application.DTOs;

namespace KindomHospital.Application.Services
{
    public class DoctorService(IDoctorRepository doctorRepository, DoctorMapper doctorMapper, ILogger<DoctorService> logger)
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
    }
}