using KindomHospital.Application.DTOs;
using KindomHospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace KindomHospital.Application.Mappers
{
    [Mapper]
    public partial class DoctorMapper
    {
        [MapperIgnoreSource(nameof(Doctor.Consultations))]
        [MapperIgnoreSource(nameof(Doctor.Ordonnances))]
        [MapperIgnoreSource(nameof(Doctor.Specialty))]
        public partial DoctorDto ToDto(Doctor entity);

        [MapperIgnoreTarget(nameof(Doctor.Consultations))]
        [MapperIgnoreTarget(nameof(Doctor.Ordonnances))]
        [MapperIgnoreTarget(nameof(Doctor.Specialty))]
        public partial Doctor ToEntity(DoctorDto dto);

        [MapperIgnoreTarget(nameof(Doctor.DoctorId))]
        [MapperIgnoreTarget(nameof(Doctor.Consultations))]
        [MapperIgnoreTarget(nameof(Doctor.Ordonnances))]
        [MapperIgnoreTarget(nameof(Doctor.Specialty))]
        public partial Doctor ToEntity(CreateDoctorDto dto);

    }
}