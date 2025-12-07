using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _oserv;
        private readonly ICacheService _cs;

        public OrderController(IOrderService oserv, ICacheService cs)
        {
            _oserv = oserv;
            _cs = cs;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = _cs.GetData<ResponseDto<List<ResOrderDto>>>("orders");

            if (res != null)
            {
                return Ok(res);
            }

            var result = await _oserv.GetAllAsync();

            _cs.SetData("orders", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var res = _cs.GetData<ResponseDto<ResOrderDto>>($"order_{id}");

            if (res != null)
            {
                return Ok(res);
            }
            var result = await _oserv.GetByIdAsync(id);
            _cs.SetData($"order_{id}", result, DateTimeOffset.Now.AddMinutes(5));
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReqOrderDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _oserv.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqOrderDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _oserv.UpdateAsync(id, dto);

            _cs.RemoveData(_cs.GetData<string>($"order_{id}"));

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
            var result = await _oserv.DeleteAsync(id);

            _cs.RemoveData(_cs.GetData<string>($"order_{id}"));

            return Ok(result);
        }

    }
}
