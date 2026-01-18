using KindomHospital.Application.DTOs;
using KindomHospital.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindomHospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/consultations")]
    public class ConsultationController(ConsultationService service, OrdonnanceService ordonnanceService, ILogger<ConsultationController> logger) : ControllerBase
    {
        private readonly ILogger<ConsultationController> _logger = logger;
        private readonly OrdonnanceService _ordonnanceService = ordonnanceService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationDto>>> GetAll([FromQuery] int? doctorId = null, [FromQuery] int? patientId = null, [FromQuery] DateOnly? from = null, [FromQuery] DateOnly? to = null)
        {
            _logger.LogInformation("Getting consultations list with filters doctorId={DoctorId} patientId={PatientId} from={From} to={To}", doctorId, patientId, from, to);

            if (!doctorId.HasValue && !patientId.HasValue)
            {
                if (from.HasValue || to.HasValue)
                    return BadRequest("At least one of doctorId or patientId must be provided when using date filters.");
                var items = await service.GetAllConsultationsAsync();
                return Ok(items);
            }

            var itemsFiltered = await service.GetFilteredConsultationsAsync(doctorId, patientId, from, to);
            if (itemsFiltered is null)
                return NotFound();

            return Ok(itemsFiltered);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultationDto>> GetById(int id)
        {
            _logger.LogInformation("Getting a Consultation by its ID");
            var item = await service.GetConsultationById(id);

            if (item is null)
            {
                _logger.LogWarning("Consultation with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("{id}/ordonnances")]
        public async Task<ActionResult<IEnumerable<OrdonnanceDto>>> GetOrdonnances(int id)
        {
            _logger.LogInformation("Getting ordonnances for consultation {Id}", id);

            var items = await _ordonnanceService.GetOrdonnancesByConsultationAsync(id);
            if (items is null)
                return NotFound();

            return Ok(items);
        }

        [HttpPost("{id}/ordonnances")]
        public async Task<ActionResult> PostOrdonnance(int id, [FromBody] CreateOrdonnanceDto dto)
        {
            _logger.LogInformation("Creating an Ordonnance for consultation {Id}", id);

            int result = await _ordonnanceService.AddForConsultation(id, dto);
            if (result == -1)
                return BadRequest("Could not create ordonnance. FK invalid (doctor/patient/consultation).");

            var item = await _ordonnanceService.GetOrdonnanceById(result);
            if (item is null)
                return NotFound();

            return CreatedAtAction("GetById", "Ordonnance", new { id = result }, item);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateConsultationDto dto)
        {
            _logger.LogInformation("Creating a new Consultation");

            int id = await service.Add(dto);
            if (id == -1)
            {
                return BadRequest("Could not create the consultation.");
            }
            var item = await service.GetConsultationById(id);
            if (item is null)
                return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateConsultationDto dto)
        {
            _logger.LogInformation("Updating Consultation with ID {Id}", id);

            int result = await service.Update(id, dto);

            if (result == -1)
            {
                return BadRequest("Could not update the consultation. Doctor or Patient does not exist.");
            }

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}