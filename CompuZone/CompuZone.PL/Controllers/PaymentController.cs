using CompuZone.BLL.DTOs;
using CompuZone.BLL.DTOs.Payment;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _pserv;

        public PaymentController(IPaymentService pserv)
        {
            _pserv = pserv;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _pserv.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _pserv.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ReqPaymentDto dto)
        {
            var result = await _pserv.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqPaymentDto dto)
        {
            var result = await _pserv.UpdateAsync(id, dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _pserv.DeleteAsync(id);
            return Ok(result);
        }


    }
}
