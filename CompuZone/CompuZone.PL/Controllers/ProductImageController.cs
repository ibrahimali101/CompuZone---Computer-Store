using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.ProductImage;
using CompuZone.BLL.DTOs.Response;
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
        private readonly ICacheService _cs;
        public ProductImageController(IProductImageService pirepo, ICacheService cs)
        {
            _pirepo = pirepo;
            _cs = cs;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pParams)
        {

            var res = _cs.GetData<ResponseDto<List<ResProductImageDto>>>("productimages");


            if (res != null)
            {
                return Ok(res);
            }

            var result = await _pirepo.GetAllAsync(pParams);

            _cs.SetData("productimages", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok();
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

            _cs.RemoveData(_cs.GetData<string>($"productimage_{id}"));

            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = _cs.GetData<ResponseDto<ResProductImageDto>>($"productimage_{id}");
            if (res != null) 
            {
                return Ok(res);
            }


            var result = await _pirepo.GetByIdAsync(id);

            _cs.SetData($"productimage_{id}", result, DateTimeOffset.Now.AddMinutes(5));
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

            _cs.SetData($"productimage_{id}", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }
    }
}
