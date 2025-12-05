using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _oserv;

        public OrderController(IOrderService oserv)
        {
            _oserv = oserv;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _oserv.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _oserv.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ReqOrderDto dto)
        {
            var result = await _oserv.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqOrderDto dto)
        {
            var result = await _oserv.UpdateAsync(id, dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _oserv.DeleteAsync(id);
            return Ok(result);
        }

    }
}
