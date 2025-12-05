using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
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
            var result = await _service.GetAllAsync();
            return Ok(result); // Returns HTTP 200 with the JSON list
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReqProductDto dto)
        {
            // The [ApiController] attribute automatically checks Validation 
            // (like [Required]) so you don't need "if (!ModelState.IsValid)"

            var result = await _service.CreateAsync(dto);

            return Ok(result);
            // Or strictly: return CreatedAtAction(nameof(GetById), new { id = ... }, dto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ReqProductDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }

    }
}
