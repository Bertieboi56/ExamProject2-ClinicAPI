using BackendNetwork.Models;
using BackendNetwork.Data;
using BackendNetwork.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BackendNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClinicController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public ClinicController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        /// <summary>
        /// Gets all Clinics.
        /// </summary>
        /// <response code="200">Returns all Clinics</response>
        /// <response code="404">Clinics not found</response>
          [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicDto>>> GetClinic()
        {
            if (_dataContext.Clinics == null)
            {
                return NotFound();
            }
            return await _dataContext.Clinics
            .Select(d=> new ClinicDto
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToListAsync();
    }

        /// <summary>
        /// Gets Clinics by Id.
        /// </summary>
        /// <response code="200">Returns the requested Clinic</response>
        /// <response code="404">Clinic not found</response>
       [HttpGet("{id}")]
       public async Task<ActionResult<ClinicDto>> GetClinicById(int id)
        {
            var clinic = await _dataContext.Clinics.FindAsync(id);
            if(clinic == null)
            {
                return NotFound();
            }
            return new ClinicDto
            {
                Id = clinic.Id,
                Name = clinic.Name
            };
        }  


        /// <summary>
        /// Adds new Clinic.
        /// </summary>
        /// <response code="200">Returns new Clinic</response>
        /// <response code="400">Clinic not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
         
        public async Task<ActionResult<ClinicDto>> PostClinic(CreateClinicDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest("Clinic type name can't be null or an empty value");
            }
            if (await _dataContext.Clinics.AnyAsync(c => c.Name == dto.Name))
            {
                return BadRequest("There is already a Clinic with this name");
            }
            var clinicType = new Clinic
            {
                Name = dto.Name
            };
            _dataContext.Clinics.Add(clinicType);
            await _dataContext.SaveChangesAsync();

            var response = new ClinicDto
            {
                Id = clinicType.Id,
                Name = clinicType.Name
            };
            return CreatedAtAction(nameof(GetClinicById), new { id = clinicType.Id}, response);
        }


        /// <summary>
        /// updates Clinics by Id.
        /// </summary>
        /// <response code="200">Returns new updated Clinic</response>
        /// <response code="404">Clinic not found</response>
        [HttpPut("{Id:int}")]

        public async Task<ActionResult> UpdateClinic(int Id, UpdateClinicDto dto)
        {
            var clinic = await _dataContext.Clinics.FindAsync(Id);
            if(clinic == null)
            {
                return NotFound();
            }
            clinic.Name = dto.Name;

            await _dataContext.SaveChangesAsync();

            return NoContent();

        }


        /// <summary>
        /// Deletes Clinics by Id.
        /// </summary>
        /// <response code="200">Returns the requested Clinic</response>
        /// <response code="404">Clinic not found</response>
        [HttpDelete("{id}")]
        
        public async Task<ActionResult> DeleteClinic(int id)
        {
            var clinic = await _dataContext.Clinics.FindAsync(id);
            if(clinic == null)
            {
                return NotFound();
            }
            var hasDoctor = await _dataContext.Doctors.AnyAsync(a => a.ClinicId == id);

            if(hasDoctor)
            {
                return BadRequest("Clinic cant be deleted,Still in use by doctor");
            }
            _dataContext.Clinics.Remove(clinic);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

      }
}