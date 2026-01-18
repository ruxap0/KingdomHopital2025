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

        [HttpGet("{id}/consultations")]
        public async Task<ActionResult<IEnumerable<ConsultationDto>>> GetConsultations(int id, [FromQuery] DateOnly? from = null, [FromQuery] DateOnly? to = null)
        {
            _logger.LogInformation("Getting consultations for doctor {Id} between {From} and {To}", id, from, to);

            var items = await service.GetConsultationsByDoctorAsync(id, from, to);
            if (items is null)
                return NotFound();

            return Ok(items);
        }

        [HttpGet("{id}/patients")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients(int id)
        {
            _logger.LogInformation("Getting patients for doctor {Id}", id);

            var items = await service.GetPatientsByDoctorAsync(id);
            if (items is null)
                return NotFound();

            return Ok(items);
        }

        [HttpGet("{id}/ordonnances")]
        public async Task<ActionResult<IEnumerable<OrdonnanceDto>>> GetOrdonnances(int id, [FromQuery] DateOnly? from = null, [FromQuery] DateOnly? to = null)
        {
            _logger.LogInformation("Getting ordonnances for doctor {Id} between {From} and {To}", id, from, to);

            var items = await service.GetOrdonnancesByDoctorAsync(id, from, to);
            if (items is null)
                return NotFound();

            return Ok(items);
        }

        [HttpGet("{id}/specialty")]
        public async Task<ActionResult<SpecialtyDto>> GetSpecialty(int id)
        {
            _logger.LogInformation("Getting Specialty for Doctor with ID {Id}", id);
            var item = await service.GetSpecialtyByDoctorAsync(id);

            if (item is null)
            {
                _logger.LogWarning("Specialty for Doctor ID {Id} not found", id);
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut("{id}/specialty/{specialtyId}")]
        public async Task<ActionResult> ChangeSpecialty(int id, int specialtyId)
        {
            _logger.LogInformation("Changing Specialty for Doctor {Id} to {SpecialtyId}", id, specialtyId);

            int result = await service.ChangeSpecialty(id, specialtyId);

            if (result == -1)
            {
                return BadRequest("Could not change specialty. Specialty does not exist.");
            }

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateDoctorDto dto)
        {
            _logger.LogInformation("Updating Doctor with ID {Id}", id);

            int result = await service.Update(id, dto);

            if (result == -1)
            {
                return BadRequest("Could not update the doctor. Specialty does not exist.");
            }

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}