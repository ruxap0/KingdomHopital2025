using KindomHospital.Application.DTOs;
using KindomHospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace KindomHospital.Application.Mappers
{
    [Mapper]
    public partial class ConsultationMapper
    {
        [MapperIgnoreSource(nameof(Consultation.Ordonnances))]
        [MapperIgnoreSource(nameof(Consultation.Doctor))]
        [MapperIgnoreSource(nameof(Consultation.Patient))]
        public partial ConsultationDto ToDto(Consultation entity);

        [MapperIgnoreTarget(nameof(Consultation.Ordonnances))]
        [MapperIgnoreTarget(nameof(Consultation.Doctor))]
        [MapperIgnoreTarget(nameof(Consultation.Patient))]
        public partial Consultation ToEntity(ConsultationDto dto);
    }
}