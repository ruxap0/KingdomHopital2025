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
    }
}