using BackendNetwork.Models;
using BackendNetwork.Data;
using BackendNetwork.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BackendNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        
        /// <summary>
        /// Gets all Categories.
        /// </summary>
        /// <response code="200">Returns all Categories</response>
        /// <response code="404">Categories not found</response>
          [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            if (_dataContext.Categories == null)
            {
                return NotFound();
            }
            return await _dataContext.Categories
            .Select(d=> new CategoryDto
            {
                Id = d.id,
                Name = d.Name
            })
            .ToListAsync();
         }


        /// <summary>
        /// Gets  by Id.
        /// </summary>
        /// <response code="200">Returns the requested Category</response>
        /// <response code="404">Category not found</response>
       [HttpGet("{id}")]
       public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _dataContext.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            return new CategoryDto
            {
                Id = category.id,
                Name = category.Name
            };
        }  

        
        /// <summary>
        /// Adds new Category.
        /// </summary>
        /// <response code="200">Returns the new Category</response>
        /// <response code="400">Category not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<CategoryDto>> PostCategory(CreateCategoryDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest("Category type name can't be null or an empty value");
            }
            if (await _dataContext.Categories.AnyAsync(c => c.Name == dto.Name))
            {
                return BadRequest("There is already a Category with this name");
            }
            var categoryType = new Category
            {
                Name = dto.Name
            };
            _dataContext.Categories.Add(categoryType);
            await _dataContext.SaveChangesAsync();

            var response = new CategoryDto
            {
                Id = categoryType.id,
                Name = categoryType.Name
            };
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryType.id}, response );
        }


        /// <summary>
        /// updates Category.
        /// </summary>
        /// <response code="200">Returns the new Category</response>
        /// <response code="404">Category not found</response>
        [HttpPut("{Id:int}")]
        public async Task<ActionResult> UpdateCategory(int Id, UpdateCategoryDto dto)
        {
            var category = await _dataContext.Categories.FindAsync(Id);
            
            if(category == null)
            {
                return NotFound();
            }
            category.Name = dto.Name;

            await _dataContext.SaveChangesAsync();

            return NoContent();



        }



         /// <summary>
        /// deletes Category by id.
        /// </summary>
        /// <response code="200">returns confirmation of deleted category</response>
        /// <response code="404">Category not found</response>
        [HttpDelete("{id}")]
        
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _dataContext.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            var hasAppointment = await _dataContext.Appointments.AnyAsync(a => a.CategoryId == id);

            if(hasAppointment)
            {
                return BadRequest("Category cant be deleted,still in use by an appointment.");
            }
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
      }
}