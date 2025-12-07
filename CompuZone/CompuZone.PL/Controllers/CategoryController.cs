using CompuZone.BLL.DTOs.Category;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _cserv;
        public CategoryController(ICategoryService cserv)
        {
            _cserv = cserv;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _cserv.GetAllAsync();
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReqCategoryDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _cserv.CreateAsync(dto);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {

            var result = await _cserv.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqCategoryDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _cserv.UpdateAsync(id, dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _cserv.DeleteAsync(id);
            return Ok(result);
        }
    }
}
