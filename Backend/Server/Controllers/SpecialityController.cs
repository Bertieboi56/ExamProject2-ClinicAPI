using BackendNetwork.Models;
using BackendNetwork.Data;
using BackendNetwork.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BackendNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SpecialityController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public SpecialityController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        /// <summary>
        /// Gets all specialities.
        /// </summary>
        /// <response code="200">Returns all Specialities</response>
        /// <response code="404">Specialites not found</response>
          [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialityDto>>> GetSpeciality()
        {
            if (_dataContext.Specialities == null)
            {
                return NotFound();
            }
            return await _dataContext.Specialities
            .Select(d=> new SpecialityDto
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToListAsync();
    }

        /// <summary>
        /// Gets specialitiy by Id.
        /// </summary>
        /// <response code="200">Returns the requested speciality</response>
        /// <response code="404">Speciality not found</response>
       [HttpGet("{id}")]
       public async Task<ActionResult<SpecialityDto>> GetSpecialityById(int id)
        {
            var speciality = await _dataContext.Specialities.FindAsync(id);
            if(speciality == null)
            {
                return NotFound();
            }
            return new SpecialityDto
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
        }  

        /// <summary>
        /// Adds new speciality.
        /// </summary>
        /// <response code="200">Returns the new speciality</response>
        /// <response code="400">Invalid request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<SpecialityDto>> PostCategory(CreateSpecialityDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest("Speciality name can't be null or an empty value");
            }
            if (await _dataContext.Specialities.AnyAsync(c => c.Name == dto.Name))
            {
                return BadRequest("There is already a Speciality with this name");
            }
            var speciality = new Speciality 
            {
                Name = dto.Name
            };
            _dataContext.Specialities.Add(speciality);
            await _dataContext.SaveChangesAsync();

            var response = new SpecialityDto
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
            return CreatedAtAction(nameof(GetSpecialityById), new { id = speciality.Id}, response );
        }


          /// <summary>
        /// Updates speciality by Id.
        /// </summary>
        /// <response code="200">Returns the requested speciality</response>
        /// <response code="404">Speciality not found</response>
        [HttpPut("{Id:int}")]

        public async Task<ActionResult> UpdateSpeciality(int Id, UpdateSpecialityDto dto)
        {
            var speciality = await _dataContext.Specialities.FindAsync(Id);
            if(speciality == null)
            {
                return NotFound();
            }
            speciality.Name = dto.Name;

            await _dataContext.SaveChangesAsync();

            return NoContent();




        }
          /// <summary>
        /// Deletes speciality by Id.
        /// </summary>
        /// <response code="200">Deletes speciality by Id</response>
        /// <response code="404">Speciality not found</response>
        [HttpDelete("{id}")]
        
        public async Task<ActionResult> DeleteSpeciality(int id)
        {
            var speciality = await _dataContext.Specialities.FindAsync(id);
            if(speciality == null)
            {
                return NotFound();
            }
            var hasDoctor = await _dataContext.Doctors.AnyAsync(a => a.SpecialityId == id);

            if(hasDoctor)
            {
                return BadRequest("Speciaity cant be deleted,still in use by a Doctor.");
            }
            _dataContext.Specialities.Remove(speciality);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
      }
}