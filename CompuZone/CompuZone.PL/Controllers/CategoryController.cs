using CompuZone.BLL.DTOs.Category;
using CompuZone.BLL.DTOs.Response;
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
        private readonly ICacheService _cs;
        public CategoryController(ICategoryService cserv, ICacheService cs)
        {
            _cserv = cserv;
            _cs = cs;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = _cs.GetData<ResponseDto<List<ResCategoryDto>>>("categories");

            if (res != null)
            {
                return Ok(res);
            }

            var result = await _cserv.GetAllAsync();

            _cs.SetData("categories", result, DateTimeOffset.Now.AddMinutes(5));

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
            var res = _cs.GetData<ResponseDto<ResCategoryDto>>($"category_{id}");

            if (res != null)
            {
                return Ok(res);
            }

            var result = await _cserv.GetByIdAsync(id);

            _cs.SetData($"category_{id}", result, DateTimeOffset.Now.AddMinutes(5));

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

            _cs.RemoveData(_cs.GetData<string>($"category_{id}"));


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

            _cs.RemoveData(_cs.GetData<string>($"category_{id}"));

            var result = await _cserv.DeleteAsync(id);
            return Ok(result);
        }
    }
}
