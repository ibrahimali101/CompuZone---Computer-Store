using CompUZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CompuZoneContext _context;

        public CategoriesController(CompuZoneContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }
    }
}