using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        // INJECTION: We ask for the Service (Interface), not the class
        public ProductController(IProductService service)
        {
            _service = service;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products); // Returns HTTP 200 with the JSON list
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReqProductDto dto)
        {
            // The [ApiController] attribute automatically checks Validation 
            // (like [Required]) so you don't need "if (!ModelState.IsValid)"

            await _service.CreateProductAsync(dto);

            return Ok(new { message = "Product created successfully" });
            // Or strictly: return CreatedAtAction(nameof(GetById), new { id = ... }, dto);
        }
    }
}
