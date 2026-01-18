using KindomHospital.Application.DTOs;
using KindomHospital.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindomHospital.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialtyController(SpecialtyService service, ILogger<SpecialtyController> logger) : ControllerBase
    {
        private readonly ILogger<SpecialtyController> _logger = logger;
        
        [HttpGet(Name = "GetAllSpecialties")]
        public async Task<ActionResult<IEnumerable<SpecialtyDto>>> Get()
        {
            _logger.LogInformation("Getting all specialties");
            var items = await service.GetAllSpecialtiesAsync();
            return Ok(items);
        }
    }
}
