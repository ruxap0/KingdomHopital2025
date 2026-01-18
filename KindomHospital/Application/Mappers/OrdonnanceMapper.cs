using KindomHospital.Application.DTOs;
using KindomHospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace KindomHospital.Application.Mappers
{
    [Mapper]
    public partial class OrdonnanceMapper
    {
        [MapperIgnoreSource(nameof(Ordonnance.OrdonnanceLignes))]
        [MapperIgnoreSource(nameof(Ordonnance.Doctor))]
        [MapperIgnoreSource(nameof(Ordonnance.Patient))]
        [MapperIgnoreSource(nameof(Ordonnance.Consultation))]
        public partial OrdonnanceDto ToDto(Ordonnance entity);

        [MapperIgnoreTarget(nameof(Ordonnance.OrdonnanceLignes))]
        [MapperIgnoreTarget(nameof(Ordonnance.Doctor))]
        [MapperIgnoreTarget(nameof(Ordonnance.Patient))]
        [MapperIgnoreTarget(nameof(Ordonnance.Consultation))]
        public partial Ordonnance ToEntity(OrdonnanceDto dto);
    }
}