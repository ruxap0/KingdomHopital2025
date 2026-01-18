using KindomHospital.Application.DTOs;
using KindomHospital.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindomHospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/ordonnances")]
    public class OrdonnanceController(OrdonnanceService service, ILogger<OrdonnanceController> logger) : ControllerBase
    {
        private readonly ILogger<OrdonnanceController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdonnanceDto>>> GetAll()
        {
            _logger.LogInformation("Getting all ordonnances");
            var items = await service.GetAllOrdonnancesAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdonnanceDto>> GetById(int id)
        {
            _logger.LogInformation("Getting an Ordonnance by its ID");
            var item = await service.GetOrdonnanceById(id);

            if (item is null)
            {
                _logger.LogWarning("Ordonnance with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("{id}/lignes")]
        public async Task<ActionResult<IEnumerable<OrdonnanceLigneDto>>> GetLignes(int id)
        {
            _logger.LogInformation("Getting lines for ordonnance {Id}", id);

            var items = await service.GetLignesByOrdonnanceAsync(id);
            if (items is null)
                return NotFound();

            return Ok(items);
        }

        [HttpPost("{id}/lignes")]
        public async Task<ActionResult> PostLignes(int id, [FromBody] IEnumerable<CreateOrdonnanceLigneDto> dtos)
        {
            _logger.LogInformation("Adding lines to ordonnance {Id}", id);

            int result = await service.AddLignesToOrdonnanceAsync(id, dtos);
            if (result == 0)
                return NotFound();
            if (result == -1)
                return BadRequest("Could not add lines. FK invalid (ordonnance or medicament).");

            var ord = await service.GetOrdonnanceById(id);
            return CreatedAtAction(nameof(GetById), new { id = id }, ord);
        }

        [HttpGet("{id}/lignes/{ligneId}")]
        public async Task<ActionResult<OrdonnanceLigneDto>> GetLigne(int id, int ligneId)
        {
            _logger.LogInformation("Getting ligne {LigneId} for ordonnance {Id}", ligneId, id);

            var item = await service.GetLigneByIdAsync(id, ligneId);
            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPut("{id}/lignes/{ligneId}")]
        public async Task<ActionResult> PutLigne(int id, int ligneId, [FromBody] CreateOrdonnanceLigneDto dto)
        {
            _logger.LogInformation("Updating ligne {LigneId} for ordonnance {Id}", ligneId, id);

            int result = await service.UpdateLigneAsync(id, ligneId, dto);
            if (result == -1)
                return BadRequest("Could not update line. FK invalid (ordonnance or medicament).");
            if (result == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}/lignes/{ligneId}")]
        public async Task<ActionResult> DeleteLigne(int id, int ligneId)
        {
            _logger.LogInformation("Deleting ligne {LigneId} from ordonnance {Id}", ligneId, id);

            int result = await service.DeleteLigneAsync(id, ligneId);
            if (result == 0)
                return NotFound();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrdonnanceDto dto)
        {
            _logger.LogInformation("Creating a new Ordonnance");

            int id = await service.Add(dto);
            if (id == -1)
            {
                return BadRequest("Could not create the ordonnance.");
            }
            var item = await service.GetOrdonnanceById(id);
            if (item is null)
                return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateOrdonnanceDto dto)
        {
            _logger.LogInformation("Updating Ordonnance with ID {Id}", id);

            int result = await service.Update(id, dto);

            if (result == -1)
            {
                return BadRequest("Could not update the ordonnance. FK invalid (doctor/patient/consultation).");
            }

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}/consultation/{consultationId}")]
        public async Task<ActionResult> AttachConsultation(int id, int consultationId)
        {
            _logger.LogInformation("Attaching Consultation {Cid} to Ordonnance {Id}", consultationId, id);

            int result = await service.AttachToConsultation(id, consultationId);

            if (result == -1)
                return BadRequest("Could not attach: consultation does not exist.");

            if (result == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}/consultation")]
        public async Task<ActionResult> DetachConsultation(int id)
        {
            _logger.LogInformation("Detaching consultation from Ordonnance {Id}", id);

            int result = await service.DetachFromConsultation(id);

            if (result == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting Ordonnance with ID {Id}", id);

            int result = await service.Delete(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdonnanceDto>>> GetAll([FromQuery] int? doctorId = null, [FromQuery] int? patientId = null, [FromQuery] DateOnly? from = null, [FromQuery] DateOnly? to = null)
        {
            _logger.LogInformation("Getting ordonnances list with filters doctorId={DoctorId} patientId={PatientId} from={From} to={To}", doctorId, patientId, from, to);

            if (!doctorId.HasValue && !patientId.HasValue)
            {
                if (from.HasValue || to.HasValue)
                    return BadRequest("At least one of doctorId or patientId must be provided when using date filters.");
                var items = await service.GetAllOrdonnancesAsync();
                return Ok(items);
            }

            var itemsFiltered = await service.GetFilteredOrdonnancesAsync(doctorId, patientId, from, to);
            if (itemsFiltered is null)
                return NotFound();

            return Ok(itemsFiltered);
        }
    }
}