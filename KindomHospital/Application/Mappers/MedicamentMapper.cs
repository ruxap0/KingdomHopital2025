using KindomHospital.Application.DTOs;
using KindomHospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace KindomHospital.Application.Mappers
{
    [Mapper]
    public partial class MedicamentMapper
    {
        [MapperIgnoreSource(nameof(Medicament.OrdonnanceLignes))]
        public partial MedicamentDto ToDto(Medicament entity);

        [MapperIgnoreTarget(nameof(Medicament.OrdonnanceLignes))]
        public partial Medicament ToEntity(MedicamentDto dto);
    }
}