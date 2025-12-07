using CompuZone.BLL.DTOs;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _oiserv;
        private readonly ICacheService _cs;
        public OrderItemController(IOrderItemService oiserv, ICacheService cs)
        {
            _oiserv = oiserv;
            _cs = cs;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pParams)
        {
            var res = _cs.GetData<ResponseDto<List<ResOrderItemDto>>>("orderitems"); 

            if (res != null)
            {
                return Ok(res);
            }

            var result = await _oiserv.GetAllAsync(pParams);

            _cs.SetData("orderitems", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }
        [HttpGet("{orderid}/{productid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int orderid, int productid)
        {
            var res = _cs.GetData<ResponseDto<ResOrderItemDto>>($"orderitem_{orderid}_{productid}");

            if (res != null)
            {
                return Ok(res);
            }

            var result = await _oiserv.GetByIdAsync(orderid, productid);

            _cs.SetData($"orderitem_{orderid}_{productid}", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReqOrderItemDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _oiserv.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqOrderItemDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _oiserv.UpdateAsync(id, dto);

            _cs.RemoveData(_cs.GetData<string>($"orderitem_{id}"));

            return Ok(result);
        }
        [HttpDelete("{orderid}/{productid}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] int orderid, int productid)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _oiserv.DeleteAsync(orderid, productid);

            _cs.RemoveData(_cs.GetData<string>($"orderitem_{orderid}_{productid}"));

            return Ok(result);
        }
    }
}
