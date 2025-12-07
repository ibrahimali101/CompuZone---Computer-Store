using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.DTOs.Shipping;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;
        private readonly ICacheService _cs;

        public ShippingController(IShippingService shippingService, ICacheService cs)
        {
            _shippingService = shippingService;
            _cs = cs;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = _cs.GetData<ResponseDto<List<ResShippingDto>>>("shippings");

            if (res != null)
            {
                return Ok(res);
            }
            var result = await _shippingService.GetAllAsync();

            _cs.SetData("shippings", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var res = _cs.GetData<ResponseDto<ResShippingDto>>($"shipping_{id}");

            if (res != null)
                {
                return Ok(res);
            }
            var result = await _shippingService.GetByIdAsync(id);

            _cs.SetData($"shipping_{id}", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReqShippingDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _shippingService.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqShippingDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _shippingService.UpdateAsync(id, dto);

            _cs.RemoveData(_cs.GetData<string>($"shipping_{id}"));

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
            var result = await _shippingService.DeleteAsync(id);

            _cs.RemoveData(_cs.GetData<string>($"shipping_{id}"));

            return Ok(result);
        }
    }
}
