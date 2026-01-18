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
    }
}