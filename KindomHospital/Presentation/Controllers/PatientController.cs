using KindomHospital.Application.DTOs;
using KindomHospital.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindomHospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController(PatientService service, ILogger<PatientController> logger) : ControllerBase
    {
        private readonly ILogger<PatientController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            _logger.LogInformation("Getting all patients");
            var items = await service.GetAllPatientsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            _logger.LogInformation("Getting a Patient by its ID");
            var item = await service.GetPatientById(id);

            if (item is null)
            {
                _logger.LogWarning("Patient with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(item);
        }
    }
}