using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindSampleAPI.Models;

namespace NorthwindSampleAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
        private readonly NorthwindContext _context;

        public CategoryController(NorthwindContext context)
        {
            _context = context;
        }
        
		// GET: api/category
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
		{
			return await _context.Categories.ToListAsync();
		}
		
		//GET: api/category/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(short id)
		{
			Category? category = await _context.Categories.FindAsync(id);
			
			if (category == null)
			{
				return NotFound();
			}

			return new Category();
		}
		
		//POST: api/category
		[HttpPost]
		public async Task<ActionResult<Category>> PostCategory(Category category)
		{
			await _context.Categories.AddAsync(category);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (CategoryExists(category.CategoryId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetCategory", new {id = category.CategoryId}, category);
		}
		
		//PUT: api/category/1
		[HttpPut("{id}")]
		public async Task<ActionResult<Category>> PutCategory(short id, Category category)
		{
			if (id != category.CategoryId)
			{
				return BadRequest();
			}
			
			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (!CategoryExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}
		
		//DELETE: api/category/1
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(Category category)
		{
			Category? existingCategory = await _context.Categories.FindAsync(category);

			if (existingCategory == null)
			{
				return NotFound();
			}

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

			return NoContent();
		}
		
		private bool CategoryExists(short id)
		{
			return _context.Categories.Any(category => category.CategoryId == id);
		}
	}
}