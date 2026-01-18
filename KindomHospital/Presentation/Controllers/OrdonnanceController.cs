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
    }
}