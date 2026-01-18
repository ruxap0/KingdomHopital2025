using KindomHospital.Application.DTOs;
using KindomHospital.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindomHospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/consultations")]
    public class ConsultationController(ConsultationService service, ILogger<ConsultationController> logger) : ControllerBase
    {
        private readonly ILogger<ConsultationController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationDto>>> GetAll()
        {
            _logger.LogInformation("Getting all consultations");
            var items = await service.GetAllConsultationsAsync();
            return Ok(items);
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