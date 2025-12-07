using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.DTOs.Response;
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
        private readonly ICacheService _cs;
        public ProductController(IProductService service, ICacheService cs)
        {
            _service = service;
            _cs = cs;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pParams)
        {
            var res = _cs.GetData<ResponseDto<List<ResProductDto>>>("products");


            if (res != null)
            {
                return Ok(res);
            }

            var result = await _service.GetAllAsync(pParams);

            _cs.SetData("products", result, DateTimeOffset.Now.AddMinutes(5));

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
            var res = _cs.GetData<ResponseDto<ResProductDto>>($"product_{id}");

            if (res != null)
            {
                return Ok(res);
            }


            var result = await _service.GetByIdAsync(id);

            _cs.SetData($"product_{id}", result, DateTimeOffset.Now.AddMinutes(5));

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

            _cs.RemoveData(_cs.GetData<string>($"product_{id}"));

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

            _cs.RemoveData(_cs.GetData<string>($"product_{id}"));   

            return Ok(result);
        }

    }
}
