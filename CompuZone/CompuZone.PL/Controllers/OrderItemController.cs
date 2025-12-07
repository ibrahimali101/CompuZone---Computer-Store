using CompuZone.BLL.DTOs;
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
        public OrderItemController(IOrderItemService oiserv)
        {
            _oiserv = oiserv;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _oiserv.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{orderid}/{productid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int orderid, int productid)
        {
            var result = await _oiserv.GetByIdAsync(orderid, productid);
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
            return Ok(result);
        }
    }
}
