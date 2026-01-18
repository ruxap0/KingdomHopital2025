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
    }
}