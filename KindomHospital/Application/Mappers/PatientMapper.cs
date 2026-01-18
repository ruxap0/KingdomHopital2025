using KindomHospital.Application.DTOs;
using KindomHospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace KindomHospital.Application.Mappers
{
    [Mapper]
    public partial class PatientMapper
    {
        [MapperIgnoreSource(nameof(Patient.Consultations))]
        [MapperIgnoreSource(nameof(Patient.Ordonnances))]
        public partial PatientDto ToDto(Patient entity);

        [MapperIgnoreTarget(nameof(Patient.Consultations))]
        [MapperIgnoreTarget(nameof(Patient.Ordonnances))]
        public partial Patient ToEntity(PatientDto dto);
    }
}