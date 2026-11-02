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

    public class PatientController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public PatientController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        /// <summary>
        /// gets patient by Id.
        /// </summary>
        /// <response code="200">gets patient by Id</response>
        /// <response code="404">patient not found</response>
        [HttpGet("{id}")]

        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            var patient = await _dataContext.Patients.FindAsync(id);
            if(patient == null)
            {
                return NotFound();
            }
            return new PatientDto
            {
                Id = patient.Id,
                Firstname = patient.Firstname,
                Lastname = patient.Lastname,
                Email = patient.Email,
                Birthdate = patient.Birthdate,
                Gender = patient.Gender,
                SocialSecurityNumber = patient.SocialSecurityNumber,
                TaxNumber = patient.Taxnumber,
                Religion = patient.Religion,
                DriversLicence = patient.DriversLicense,
                MedicalInsuranceMemberNumber = patient.MedicalInsuranceMemberNumber

                
            };
        }


        /// <summary>
        /// gets all patients.
        /// </summary>
        /// <response code="200">Returns all patient</response>
        /// <response code="404">patient not found</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
            if (_dataContext.Patients == null)
            {
                return NotFound();
            }
            return await _dataContext.Patients
            .Select(d=> new PatientDto
            {
                Id = d.Id,
                Firstname = d.Firstname,
                Lastname = d.Lastname, 
                Email = d.Email,
                Birthdate = d.Birthdate,
                Gender = d.Gender,
                SocialSecurityNumber = d.SocialSecurityNumber,
                TaxNumber = d.Taxnumber,
                Religion = d.Religion,
                DriversLicence = d.DriversLicense,
                MedicalInsuranceMemberNumber = d.MedicalInsuranceMemberNumber

            })
            .ToListAsync();
         }
        
       /// <summary>
        /// Creates patient.
        /// </summary>
        /// <response code="200">Successfully creates new Patient</response>
        /// <response code="400">Request invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<PatientDto>> PostPatient(CreatePatientDto dto)
        {

            var emailAlreadyExists = await _dataContext.Patients
            .AnyAsync(p => p.Email == dto.Email);

             if (emailAlreadyExists)
            {
              return BadRequest("A patient with this email already exists.");
            }
            
            var patient = new Patient
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Email = dto.Email,
                Birthdate = dto.Birthdate,
                Gender = dto.Gender

            };
            _dataContext.Patients.Add(patient);
            await _dataContext.SaveChangesAsync();
            var response = new PatientDto
            {
                Id = patient.Id,
                Firstname = patient.Firstname,
                Lastname = patient.Lastname,
                Email = patient.Email,
                Birthdate = patient.Birthdate,
                Gender = patient.Gender
            };
            return CreatedAtAction(nameof(GetPatientById),new {id = patient.Id}, response);
        }


        /// <summary>
        /// Updates patient by Id.
        /// </summary>
        /// <response code="200">returns updated patient</response>
        /// <response code="404">patient not found</response>
        [HttpPut("{Id:int}")]

        public async Task<ActionResult> UpdatePatient(int Id, UpdatePatientDto dto)
        {
            var patient = await _dataContext.Patients.FindAsync(Id);
            if(patient == null)
            {
                return NotFound();
            }
            patient.Firstname = dto.Firstname;
            patient.Lastname = dto.Lastname;
            patient.Email = dto.Email;
            patient.Birthdate = dto.Birthdate;
            patient.Gender = dto.Gender;
            patient.SocialSecurityNumber = dto.SocialSecurityNumber;
            patient.Taxnumber = dto.TaxNumber;
            patient.Religion = dto.Religion;
            patient.DriversLicense = dto.DriversLicence;
            patient.MedicalInsuranceMemberNumber = dto.MedicalInsuranceMemberNumber;


            await _dataContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes patient by Id.
        /// </summary>
        /// <response code="200">Deletes patient by Id</response>
        /// <response code="404">patient not found</response>
        [HttpDelete("{id}")]
        
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patient = await _dataContext.Patients.FindAsync(id);
            if(patient == null)
            {
                return NotFound();
            }
           
            _dataContext.Patients.Remove(patient);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }


       
      }
}