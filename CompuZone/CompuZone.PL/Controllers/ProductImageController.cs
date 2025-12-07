using CompuZone.BLL.DTOs.ProductImage;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _pirepo;
        public ProductImageController(IProductImageService pirepo)
        {
            _pirepo = pirepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pirepo.GetAllAsync());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ReqProductImageDto pidto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _pirepo.CreateAsync(pidto);
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
            var result = await _pirepo.DeleteAsync(id);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _pirepo.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ReqProductImageDto pidto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _pirepo.UpdateAsync(id, pidto);
            return Ok(result);
        }
    }
}
