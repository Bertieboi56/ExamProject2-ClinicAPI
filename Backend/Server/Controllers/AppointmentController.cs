using BackendNetwork.Models;
using BackendNetwork.Data;
using BackendNetwork.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace BackendNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AppointmentController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public AppointmentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        /// <summary>
        /// gets appointment by Id.
        /// </summary>
        /// <response code="200">returns patient by Id</response>
        /// <response code="404">patient not found</response>
        [HttpGet("{id}")]

        public async Task<ActionResult<GetAppointmentDto>> GetAppointmentById(int id)
        {
             var appointment = await _dataContext.Appointments
            .Include(a => a.Doctor)
            .ThenInclude(d => d!.Clinic)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == id);
            if(appointment == null)
            {
                return NotFound();
            }
            return new GetAppointmentDto
            {
                 Id = appointment.Id,
                 Date = appointment.Date,
                 Duration = appointment.Duration,
                 DoctorId = appointment.DoctorId,
                 CategoryId = appointment.CategoryId,

                 DoctorName = appointment.Doctor!.Firstname + " " + appointment.Doctor.Lastname,
                ClinicName = appointment.Doctor.Clinic!.Name,
                CategoryName = appointment.Category!.Name
                };
        }


        /// <summary>
        /// Gets all Appointments.
        /// </summary>
        /// <response code="200">Returns all Appointments</response>
        /// <response code="404">Appointments not found</response>
          
          [HttpGet]
          [Authorize]
        public async Task<ActionResult<IEnumerable<GetAppointmentDto>>> GetAppointments()
        {
            var patientId = int.Parse(User.FindFirst("patientId")!.Value);

            if (_dataContext.Appointments == null)
            {
                return NotFound();
            }
            return await _dataContext.Appointments
            .Where(a => a.PatientId == patientId)
            .Select(d=> new GetAppointmentDto
            {
                Id = d.Id,
                Date = d.Date,
                DoctorName = d.Doctor!.Firstname + " " + d.Doctor.Lastname,
                ClinicName = d.Doctor!.Clinic!.Name,
                CategoryName = d.Category!.Name,
                Duration = d.Duration

            })
            .ToListAsync();
    }

 /// <summary>
/// Creates a new appointment for a registered patient or guest.
/// </summary>
/// <remarks>
/// Registered users: patient information is taken from the JWT using the patient id claim in authservice/ patient record
/// Firstname, Lastname and birthdate do not need to be included in the request body,these values are ignored.<br />
/// Guests: firstname, lastname and birthdate are required as guests are not supplied with a token
/// </remarks>

[HttpPost]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public async Task<ActionResult<AppointmentDto>> PostAppointment(CreateAppointmentDto dto)
{
    // VALIDATION
   //If the duration is less than 0 will throw an error.
    if (dto.Duration <= 0)
    {
        return BadRequest(new
        {
            message = "Duration must be greater than 0."
        });
    }

    //If the date is not current or a future date, will throw an error.
    if(dto.Date < DateTime.Now)
            {
                return BadRequest(new{message="Appointment date cannot be in the past."});
            }
            
    var category = await _dataContext.Categories
        .FirstOrDefaultAsync(c => c.id == dto.CategoryId);

    if (category == null)
    {
        return BadRequest(new
        {
            message = "A category must be selected below"
        });
    }

    // Check that doctor exists
    var doctor = await _dataContext.Doctors
        .FirstOrDefaultAsync(d => d.Id == dto.DoctorId);

    if (doctor == null)
    {
        return BadRequest(new {   message = "A Doctor must be selected" });
    }

    int patientId = 0;
    var patientIdClaim = User.FindFirst("patientId");

    Patient? guestPatient = null;

    if (patientIdClaim != null)
    {
        if (!int.TryParse(patientIdClaim.Value, out patientId))
        {
            return Unauthorized(new
            {
                message = "Invalid patient ID in token."
            });
        }

        // Make sure the patient still exists
        var patient = await _dataContext.Patients.FindAsync(patientId);

        if (patient == null)
        {
            return BadRequest(new
            {
                message = $"Patient with id {patientId} does not exist."
            });
        }
    }
    else
    {
        if (string.IsNullOrWhiteSpace(dto.Firstname))
        {
            return BadRequest(new
            {
                message = "Firstname is required."
            });
        }

        if (string.IsNullOrWhiteSpace(dto.Lastname))
        {
            return BadRequest(new
            {
                message = "Lastname is required."
            });
        }

        if (!dto.Birthdate.HasValue)
        {
            return BadRequest(new
            {
                message = "Birthdate is required."
            });
        }

        // Create a Patient record for the guest
        
        guestPatient = new Patient
        {
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Birthdate = dto.Birthdate
        };

        _dataContext.Patients.Add(guestPatient);
    }

    var newEnd = dto.Date.AddMinutes(dto.Duration);

    
    // Check if registered patient already has an appointment
    // at the same clinic and time
    if (patientIdClaim != null)
    {
        var conflictAppointment = await _dataContext.Appointments
            .Include(a => a.Doctor)
            .AnyAsync(a =>
                a.PatientId == patientId &&
                a.Doctor != null &&
                a.Doctor.ClinicId == doctor.ClinicId &&
                a.Date < newEnd &&
                a.Date.AddMinutes(a.Duration) > dto.Date
            );

        if (conflictAppointment)
        {
            return BadRequest(new
            {
                message = "You already have an appointment at this time with the clinic."
            });
        }
    }

    // Check if the doctor is already booked
    var doctorConflict = await _dataContext.Appointments
        .AnyAsync(a =>
            a.DoctorId == dto.DoctorId &&
            a.Date < newEnd &&
            a.Date.AddMinutes(a.Duration) > dto.Date
        );

    if (doctorConflict)
    {
        return BadRequest(new
        {
            message = "The doctor already has an appointment during this time."
        });
    }

    

    // Create appointment
    var appointment = new Appointment
    {
        Date = dto.Date,
        Duration = dto.Duration,
        DoctorId = dto.DoctorId,
        CategoryId = dto.CategoryId
    };

    if (patientIdClaim != null)
    {
        // Registered patient!!
        appointment.PatientId = patientId;
    }
    else
    {
        // Guest patient!!
        appointment.Patient = guestPatient!;
    }

    _dataContext.Appointments.Add(appointment);

    
    await _dataContext.SaveChangesAsync();

    
    var appointmentPatient = await _dataContext.Patients.FindAsync(appointment.PatientId);

    var response = new AppointmentDto
    {
        Id = appointment.Id,
        Date = appointment.Date,
        PatientId = appointment.PatientId,
        DoctorId = appointment.DoctorId,
        CategoryId = appointment.CategoryId,
        Duration = appointment.Duration,
        Firstname = appointmentPatient!.Firstname,
        Lastname = appointmentPatient.Lastname
    };

    return CreatedAtAction( nameof(GetAppointmentById), new { id = appointment.Id }, response
    );
}
  
        /// <summary>
        /// Updates appointments by id.
        /// </summary>
        /// <response code="200">Returns the updated Appointments</response>
        /// <response code="404">Appointments not found</response>
        [HttpPut("{Id:int}")]
        [Authorize]

        public async Task<ActionResult> UpdateAppointment(int Id, UpdateAppointmentDto dto)
        {
            var patientId = int.Parse(User.FindFirst("patientId")!.Value);

             var appointment = await _dataContext.Appointments.Include(a => a.Doctor) .FirstOrDefaultAsync(a => a.Id == Id);
            if(appointment == null)
            {
                return NotFound();
            }

            if (appointment.PatientId != patientId)
            {
                return Forbid();
            }

            if(dto.Date < DateTime.Now)
            {
                return BadRequest(new{message="Appointment date cannot be in the past."});
            }

            // Check that doctor exists
         var doctor = await _dataContext.Doctors .FirstOrDefaultAsync(d => d.Id == dto.DoctorId);

         if (doctor == null)
       {
        return BadRequest(new {   message = "A Doctor must be selected" });
       }

       var newEnd = dto.Date.AddMinutes(dto.Duration);

       var conflictAppointment = await _dataContext.Appointments
            .Include(a => a.Doctor)
            .AnyAsync(a =>
                a.PatientId == patientId &&
                a.Doctor != null &&
                a.Doctor.ClinicId == doctor.ClinicId &&
                a.Date < newEnd &&
                a.Date.AddMinutes(a.Duration) > dto.Date
            );

        if (conflictAppointment)
        {
            return BadRequest(new
            {
                message = "You already have an appointment at this time with the clinic."
            });
        }

        // Check if the doctor is already booked
    var doctorConflict = await _dataContext.Appointments
        .AnyAsync(a =>
            a.DoctorId == dto.DoctorId &&
            a.Date < newEnd &&
            a.Date.AddMinutes(a.Duration) > dto.Date
        );

    if (doctorConflict)
    {
        return BadRequest(new
        {
            message = "The doctor already has an appointment during this time."
        });
    }

            appointment.Date = dto.Date;
            appointment.DoctorId = dto.DoctorId;
            appointment.CategoryId = dto.CategoryId;
            appointment.Duration = dto.Duration;
            

            await _dataContext.SaveChangesAsync();

          return NoContent();
        }


        /// <summary>
        /// Deletes Appointment by id.
        /// </summary>
        /// <response code="204">successfully deletes the Appointment</response>
        /// <response code="404">Appointments not found</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var patientId = int.Parse(User.FindFirst("patientId")!.Value);

            var appointment = await _dataContext.Appointments.FindAsync(id);

            if(appointment == null)
            {
                return NotFound();
            }

            if(appointment.PatientId != patientId)
            {
                return Forbid();
            }

            _dataContext.Appointments.Remove(appointment);

            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
       }
 }