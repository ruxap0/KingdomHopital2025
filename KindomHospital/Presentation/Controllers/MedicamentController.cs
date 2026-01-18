using KindomHospital.Application.DTOs;
using KindomHospital.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindomHospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/medicaments")]
    public class MedicamentController(MedicamentService service, ILogger<MedicamentController> logger) : ControllerBase
    {
        private readonly ILogger<MedicamentController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentDto>>> GetAll()
        {
            _logger.LogInformation("Getting all medicaments");
            var items = await service.GetAllMedicamentsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicamentDto>> GetById(int id)
        {
            _logger.LogInformation("Getting a Medicament by its ID");
            var item = await service.GetMedicamentById(id);

            if (item is null)
            {
                _logger.LogWarning("Medicament with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("{id}/ordonnances")]
        public async Task<ActionResult<IEnumerable<OrdonnanceDto>>> GetOrdonnances(int id)
        {
            _logger.LogInformation("Getting ordonnances containing medicament {Id}", id);

            var items = await service.GetOrdonnancesByMedicamentAsync(id);
            if (items is null)
                return NotFound();

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateMedicamentDto dto)
        {
            _logger.LogInformation("Creating a new Medicament");

            int id = await service.Add(dto);
            if (id == -1)
            {
                return BadRequest("Could not create the medicament.");
            }
            var item = await service.GetMedicamentById(id);
            if (item is null)
                return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = id }, item);
        }
    }
}