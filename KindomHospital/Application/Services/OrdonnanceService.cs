using KindomHospital.Application.DTOs;
using KindomHospital.Application.Mappers;
using KindomHospital.Application.Repositories;
using KindomHospital.Infrastructure.Repositories;

namespace KindomHospital.Application.Services
{
    public class OrdonnanceService(IOrdonnanceRepository ordonnanceRepository, OrdonnanceMapper ordonnanceMapper, IOrdonnanceLigneRepository ligneRepository, OrdonnanceLigneMapper ligneMapper, IConsultationRepository consultationRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository, ILogger<OrdonnanceService> logger)
    {
        public async Task<IEnumerable<OrdonnanceDto>> GetAllOrdonnancesAsync()
        {
            logger.LogInformation("GetAllOrdonnancesAsync");
            var entities = await ordonnanceRepository.GetAllOrdonnancesAsync();
            var dtos = entities.Select(ordonnanceMapper.ToDto);
            return dtos;
        }

        public async Task<OrdonnanceDto> GetOrdonnanceById(int id)
        {
            logger.LogInformation("GetOrdonnanceById; id : " + id);
            var entity = await ordonnanceRepository.GetOrdonnanceById(id);
            OrdonnanceDto dto = entity is null ? null : ordonnanceMapper.ToDto(entity);
            return dto;
        }

        public async Task<int> Add(CreateOrdonnanceDto dto)
        {
            logger.LogInformation("Add Ordonnance");
            var entity = ordonnanceMapper.ToEntity(dto);
            return await ordonnanceRepository.AddOrdonnanceAsync(entity);
        }

        public async Task<int> Update(int id, CreateOrdonnanceDto dto)
        {
            logger.LogInformation("Update Ordonnance; id : " + id);
            var entity = ordonnanceMapper.ToEntity(dto);
            entity.OrdonnanceId = id;
            return await ordonnanceRepository.UpdateOrdonnanceAsync(entity);
        }

        public async Task<int> Delete(int id)
        {
            logger.LogInformation("Delete Ordonnance; id : " + id);
            return await ordonnanceRepository.DeleteOrdonnanceAsync(id);
        }

        public async Task<IEnumerable<OrdonnanceLigneDto>?> GetLignesByOrdonnanceAsync(int ordonnanceId)
        {
            logger.LogInformation("GetLignesByOrdonnanceAsync; ordonnanceId : " + ordonnanceId);
            var ord = await ordonnanceRepository.GetOrdonnanceById(ordonnanceId);
            if (ord is null)
                return null;

            var lignes = await ligneRepository.GetLignesByOrdonnanceAsync(ordonnanceId);
            return lignes.Select(ligneMapper.ToDto);
        }

        public async Task<OrdonnanceLigneDto?> GetLigneByIdAsync(int ordonnanceId, int ligneId)
        {
            logger.LogInformation("GetLigneByIdAsync; ordonnanceId : " + ordonnanceId + " ligneId : " + ligneId);
            var ord = await ordonnanceRepository.GetOrdonnanceById(ordonnanceId);
            if (ord is null)
                return null;

            var ligne = await ligneRepository.GetOrdonnanceLigneByIdAsync(ligneId);
            if (ligne is null || ligne.OrdonnanceId != ordonnanceId)
                return null;

            return ligneMapper.ToDto(ligne);
        }

        public async Task<int> AddLignesToOrdonnanceAsync(int ordonnanceId, IEnumerable<CreateOrdonnanceLigneDto> dtos)
        {
            logger.LogInformation("AddLignesToOrdonnanceAsync; ordonnanceId : " + ordonnanceId);
            var ord = await ordonnanceRepository.GetOrdonnanceById(ordonnanceId);
            if (ord is null)
                return 0;

            var entities = dtos.Select(d =>
            {
                var e = ligneMapper.ToEntity(d);
                e.OrdonnanceId = ordonnanceId;
                return e;
            }).ToList();

            return await ligneRepository.AddOrdonnanceLignesAsync(entities);
        }

        public async Task<int> UpdateLigneAsync(int ordonnanceId, int ligneId, CreateOrdonnanceLigneDto dto)
        {
            logger.LogInformation("UpdateLigneAsync; ordonnanceId : " + ordonnanceId + " ligneId : " + ligneId);
            var ord = await ordonnanceRepository.GetOrdonnanceById(ordonnanceId);
            if (ord is null)
                return 0;

            var existing = await ligneRepository.GetOrdonnanceLigneByIdAsync(ligneId);
            if (existing is null || existing.OrdonnanceId != ordonnanceId)
                return 0;

            var entity = ligneMapper.ToEntity(dto);
            entity.OrdonnanceLigneId = ligneId;
            entity.OrdonnanceId = ordonnanceId;
            return await ligneRepository.UpdateOrdonnanceLigneAsync(entity);
        }

        public async Task<int> DeleteLigneAsync(int ordonnanceId, int ligneId)
        {
            logger.LogInformation("DeleteLigneAsync; ordonnanceId : " + ordonnanceId + " ligneId : " + ligneId);
            var ord = await ordonnanceRepository.GetOrdonnanceById(ordonnanceId);
            if (ord is null)
                return 0;

            var existing = await ligneRepository.GetOrdonnanceLigneByIdAsync(ligneId);
            if (existing is null || existing.OrdonnanceId != ordonnanceId)
                return 0;

            return await ligneRepository.DeleteOrdonnanceLigneAsync(ligneId);
        }

        public async Task<IEnumerable<OrdonnanceDto>?> GetOrdonnancesByMedicamentAsync(int medicamentId)
        {
            logger.LogInformation("GetOrdonnancesByMedicamentAsync; medicamentId : " + medicamentId);
            var ords = await ligneRepository.GetOrdonnancesByMedicamentAsync(medicamentId);
            return ords.Select(ordonnanceMapper.ToDto);
        }

        public async Task<IEnumerable<OrdonnanceDto>?> GetOrdonnancesByConsultationAsync(int consultationId)
        {
            logger.LogInformation("GetOrdonnancesByConsultationAsync; consultationId : " + consultationId);
            var consultation = await consultationRepository.GetConsultationById(consultationId);
            if (consultation is null)
                return null;

            var ords = await ordonnanceRepository.GetOrdonnancesByConsultationAsync(consultationId);
            return ords.Select(ordonnanceMapper.ToDto);
        }

        public async Task<int> AddForConsultation(int consultationId, CreateOrdonnanceDto dto)
        {
            logger.LogInformation("AddForConsultation; consultationId : " + consultationId);
            var consultation = await consultationRepository.GetConsultationById(consultationId);
            if (consultation is null)
                return -1;

            var dtoWithConsultation = dto with { ConsultationId = consultationId };
            var entity = ordonnanceMapper.ToEntity(dtoWithConsultation);
            return await ordonnanceRepository.AddOrdonnanceAsync(entity);
        }

        public async Task<int> AttachToConsultation(int ordonnanceId, int consultationId)
        {
            logger.LogInformation("AttachToConsultation; ordonnanceId: {Id} consultationId: {Cid}", ordonnanceId, consultationId);
            return await ordonnanceRepository.AttachOrdonnanceToConsultationAsync(ordonnanceId, consultationId);
        }

        public async Task<int> DetachFromConsultation(int ordonnanceId)
        {
            logger.LogInformation("DetachFromConsultation; ordonnanceId: {Id}", ordonnanceId);
            return await ordonnanceRepository.DetachOrdonnanceFromConsultationAsync(ordonnanceId);
        }

        public async Task<IEnumerable<OrdonnanceDto>?> GetFilteredOrdonnancesAsync(int? doctorId, int? patientId, DateOnly? from, DateOnly? to)
        {
            logger.LogInformation("GetFilteredOrdonnancesAsync; doctorId: {DoctorId}, patientId: {PatientId}, from: {From}, to: {To}", doctorId, patientId, from, to);

            if (doctorId.HasValue)
            {
                var doc = await doctorRepository.GetDoctorById(doctorId.Value);
                if (doc is null)
                    return null;
            }

            if (patientId.HasValue)
            {
                var pat = await patientRepository.GetPatientById(patientId.Value);
                if (pat is null)
                    return null;
            }

            var ords = await ordonnanceRepository.GetOrdonnancesFilteredAsync(doctorId, patientId, from, to);
            return ords.Select(ordonnanceMapper.ToDto);
        }
    }
}