using KindomHospital.Application.DTOs;
using KindomHospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace KindomHospital.Application.Mappers
{
    [Mapper]
    public partial class OrdonnanceLigneMapper
    {
        [MapperIgnoreSource(nameof(OrdonnanceLigne.Ordonnance))]
        [MapperIgnoreSource(nameof(OrdonnanceLigne.Medicament))]
        public partial OrdonnanceLigneDto ToDto(OrdonnanceLigne entity);

        [MapperIgnoreTarget(nameof(OrdonnanceLigne.Ordonnance))]
        [MapperIgnoreTarget(nameof(OrdonnanceLigne.Medicament))]
        public partial OrdonnanceLigne ToEntity(OrdonnanceLigneDto dto);

        [MapperIgnoreTarget(nameof(OrdonnanceLigne.OrdonnanceLigneId))]
        [MapperIgnoreTarget(nameof(OrdonnanceLigne.Ordonnance))]
        [MapperIgnoreTarget(nameof(OrdonnanceLigne.Medicament))]
        public partial OrdonnanceLigne ToEntity(CreateOrdonnanceLigneDto dto);
    }
}