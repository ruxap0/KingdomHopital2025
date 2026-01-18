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

        [HttpGet("{id}/consultations")]
        public async Task<ActionResult<IEnumerable<ConsultationDto>>> GetConsultations(int id)
        {
            _logger.LogInformation("Getting consultations for patient {Id}", id);

            var items = await service.GetConsultationsByPatientAsync(id);
            if (items is null)
                return NotFound();

            return Ok(items);
        }

        [HttpGet("{id}/ordonnances")]
        public async Task<ActionResult<IEnumerable<OrdonnanceDto>>> GetOrdonnances(int id)
        {
            _logger.LogInformation("Getting ordonnances for patient {Id}", id);

            var items = await service.GetOrdonnancesByPatientAsync(id);
            if (items is null)
                return NotFound();

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreatePatientDto dto)
        {
            _logger.LogInformation("Creating a new Patient");

            int idPatient = await service.Add(dto);
            if (idPatient == -1)
            {
                return BadRequest("Could not create the patient.");
            }
            var item = await service.GetPatientById(idPatient);
            if (item is null)
                return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = idPatient }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreatePatientDto dto)
        {
            _logger.LogInformation("Updating Patient with ID {Id}", id);

            int result = await service.Update(id, dto);

            if (result == -1)
            {
                return BadRequest("Could not update the patient. Invalid foreign key.");
            }

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting Patient with ID {Id}", id);

            int result = await service.Delete(id);

            if (result == -1)
            {
                return BadRequest("Could not delete the patient. Related consultations or ordonnances exist.");
            }

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}