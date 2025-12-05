using CompuZone.BLL.DTOs.Category;
using CompuZone.BLL.Services.Interfaces;
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
        public async Task<IActionResult> CreateAsync([FromBody] ReqCategoryDto dto)
        {
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
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqCategoryDto dto)
        {
            var result = await _cserv.UpdateAsync(id, dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _cserv.DeleteAsync(id);
            return Ok(result);
        }
    }
}
