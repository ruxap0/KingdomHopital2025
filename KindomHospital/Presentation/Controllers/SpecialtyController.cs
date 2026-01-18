using KindomHospital.Application.DTOs;
using KindomHospital.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindomHospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/specialties")]
    public class SpecialtyController(SpecialtyService service, ILogger<SpecialtyController> logger) : ControllerBase
    {
        private readonly ILogger<SpecialtyController> _logger = logger;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialtyDto>>> GetAll()
        {
            _logger.LogInformation("Getting all specialties");
            var items = await service.GetAllSpecialtiesAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialtyDto>> GetById(int id)
        {
            _logger.LogInformation("Getting a Specialty by its ID");
            var item = await service.GetSpecialtyById(id);
            
            if (item is null)
            {
                _logger.LogWarning("Specialty with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(item);
        }
    }
}
