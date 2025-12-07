using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result); 
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ReqProductDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ReqProductDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }

    }
}
