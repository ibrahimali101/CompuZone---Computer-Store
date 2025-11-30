using CompUZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CompuZoneContext _context;

        public ProductsController(CompuZoneContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCatalog>>> GetProducts()
        {
            // بنجيب المنتجات وبنحمل معاها الصور والفئة (Category)
            return await _context.ProductCatalogs
                                 .Include(p => p.Category)
                                 .Include(p => p.ProductImages)
                                 .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCatalog>> GetProduct(int id)
        {
            var product = await _context.ProductCatalogs
                                        .Include(p => p.Category)
                                        .Include(p => p.ProductImages)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}