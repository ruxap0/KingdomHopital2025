using KindomHospital.Application.DTOs;
using KindomHospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace KindomHospital.Application.Mappers
{
    [Mapper]
    public partial class SpecialtyMapper
    {
        [MapperIgnoreSource(nameof(Specialty.Doctors))]
        public partial SpecialtyDto ToDto(Specialty entity);

        [MapperIgnoreTarget(nameof(Specialty.Doctors))]
        public partial Specialty ToEntity(SpecialtyDto dto);
    }
}
