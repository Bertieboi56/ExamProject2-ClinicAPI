using BackendNetwork.Models;
using BackendNetwork.Data;
using BackendNetwork.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace  BackendNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public DoctorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Gets all doctors.
        /// </summary>
        /// <response code="200">Returns all doctors</response>
        /// <response code="404">Doctors not found</response>
        [HttpGet]

        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctor()
        {
            if (_dataContext.Doctors == null)
            {
                return NotFound();
            }
            return await _dataContext.Doctors
            .Select(d=> new DoctorDto
            {
                Id = d.Id,
                Firstname = d.Firstname,
                Lastname = d.Lastname,
                SpecialityId = d.SpecialityId,
                ClinicId = d.ClinicId
            })
            .ToListAsync();
        }


        /// <summary>
        /// Gets Doctor by Id.
        /// </summary>
        /// <response code="200">Returns the requested Doctor</response>
        /// <response code="404">Doctor Id not found</response>
        [HttpGet("{id}")]

        public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
        {
            var doctor = await _dataContext.Doctors.FindAsync(id);
            if(doctor == null)
            {
                return NotFound();
            }
            return new DoctorDto
            {
                Id = doctor.Id,
                Firstname = doctor.Firstname,
                Lastname = doctor.Lastname,
                SpecialityId = doctor.SpecialityId,
                ClinicId = doctor.ClinicId
            };
        }


         /// <summary>
        /// Adds new doctor.
        /// </summary>
        /// <response code="200">Returns new doctors</response>
        /// <response code="40o">Doctors not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<DoctorDto>> PostDoctor(CreateDoctorDto dto)
        {
            if (string.IsNullOrEmpty(dto.Firstname))
            {
                return BadRequest("firstName is required");
            }

            if (string.IsNullOrEmpty(dto.Lastname))
            {
                return BadRequest("LastName is required");
            }

            var specialityExists = await _dataContext.Specialities
            .AnyAsync(s => s.Id == dto.SpecialityId);

            if (!specialityExists)
            {
              return BadRequest($"Speciality with id {dto.SpecialityId} does not exist.");
            }

             var clinicExists = await _dataContext.Clinics
             .AnyAsync(c => c.Id == dto.ClinicId);

            if (!clinicExists)
            {
              return BadRequest($"Clinic with id {dto.ClinicId} does not exist.");
            }

            var doctor = new Doctor
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                SpecialityId = dto.SpecialityId,
                 ClinicId = dto.ClinicId
            };

            _dataContext.Doctors.Add(doctor);
            await _dataContext.SaveChangesAsync();

            var response = new DoctorDto
             {
               Id = doctor.Id,
               Firstname = doctor.Firstname,
               Lastname = doctor.Lastname,
               SpecialityId = doctor.SpecialityId,
               ClinicId = doctor.ClinicId
            };

               return CreatedAtAction( nameof(GetDoctorById), new { id = doctor.Id }, response
            );
        }


         /// <summary>
        /// Updtes doctor by id.
        /// </summary>
        /// <response code="200">Returns updated Doctor</response>
        /// <response code="404">Doctors not found</response>
        [HttpPut("{Id:int}")]
        

        public async Task<ActionResult> UpdateAppointment(int Id, UpdateDoctorDto dto)
        {
            var doctor = await _dataContext.Doctors.FindAsync(Id);
            if(doctor == null)
            {
                return NotFound();
            }
            doctor.Firstname = dto.Firstname;
            doctor.Lastname = dto.Lastname;
            doctor.SpecialityId = dto.SpecialityId;
            doctor.ClinicId = dto.ClinicId;
            
            await _dataContext.SaveChangesAsync();

            return NoContent();

        }


         /// <summary>
        /// Deletes doctor by id.
        /// </summary>
        /// <response code="200"> Deletes speciality by doctors</response>
        /// <response code="404">Doctors not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _dataContext.Doctors.FindAsync(id);
            if(doctor == null)
            {
                return NotFound();
            }

            var hasAppointment = await _dataContext.Appointments.AnyAsync(a => a.DoctorId == id);

            if (hasAppointment)
            {
                return BadRequest("doctor can't be deleted, in use by appointment");
            }
            _dataContext.Doctors.Remove(doctor);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

         /// <summary>
        /// Searches for Doctor.
        /// </summary>
        /// <response code="200"> returns doctor results</response>
        /// <response code="404">Doctors not found</response>  
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SearchDoctorDto>>> SearchDoctors(string name)
        {
           if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Please enter a first or last name");
            } 

            var doctors = await _dataContext.Doctors
            .Include(d => d.Clinic)
            .Include(d => d.Speciality)
            .Where(d => d.Firstname.Contains(name) || d.Lastname.Contains(name)).Select(d => new SearchDoctorDto
            {
                FullName = d.Firstname + " " + d.Lastname,
                ClinicName = d.Clinic.Name,
                SpecialityName = d.Speciality.Name
            })
            .ToListAsync();
            if (doctors.Count == 0)
            {
                return NotFound(" No doctors found with that name");
            }
            return Ok(doctors);
        }
}

}