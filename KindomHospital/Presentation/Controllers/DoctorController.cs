using KindomHospital.Application.DTOs;
using KindomHospital.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KindomHospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController(DoctorService service, ILogger<DoctorController> logger) : ControllerBase
    {
        private readonly ILogger<DoctorController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAll()
        {
            _logger.LogInformation("Getting all doctors");
            var items = await service.GetAllDoctorsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetById(int id)
        {
            _logger.LogInformation("Getting a Doctor by its ID");
            var item = await service.GetDoctorById(id);

            if (item is null)
            {
                _logger.LogWarning("Doctor with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateDoctorDto dto)
        {
            _logger.LogInformation("Creating a new Doctor");

            int idDoctor = await service.Add(dto);
            if (idDoctor == -1)
            {
                return BadRequest("Could not create the doctor.");
            }
            var item = await service.GetDoctorById(idDoctor);
            if (item is null)
                return NotFound();


            return CreatedAtAction(nameof(GetById), new { id = idDoctor }, item);
        }
    }
}