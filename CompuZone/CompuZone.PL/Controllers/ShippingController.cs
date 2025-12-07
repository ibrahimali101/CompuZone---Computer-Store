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

        public ShippingController(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _shippingService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _shippingService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReqShippingDto dto)
        {
            var result = await _shippingService.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqShippingDto dto)
        {
            var result = await _shippingService.UpdateAsync(id, dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _shippingService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
