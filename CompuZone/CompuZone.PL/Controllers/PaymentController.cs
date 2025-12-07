using CompuZone.BLL.DTOs;
using CompuZone.BLL.DTOs.Payment;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _pserv;
        private readonly ICacheService _cs;
        public PaymentController(IPaymentService pserv, ICacheService cs)
        {
            _pserv = pserv;
            _cs = cs;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = _cs.GetData<ResponseDto<List<ResPaymentDto>>>("payments");

            if (res != null)
            {
                return Ok(res);
            }

            var result = await _pserv.GetAllAsync();

            _cs.SetData("payments", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var res = _cs.GetData<ResponseDto<ResPaymentDto>>($"payment_{id}");

            if (res != null)
            {
                return Ok(res);
            }

            var result = await _pserv.GetByIdAsync(id);

            _cs.SetData($"payment_{id}", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReqPaymentDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _pserv.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqPaymentDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _pserv.UpdateAsync(id, dto);

            _cs.RemoveData(_cs.GetData<string>($"payment_{id}"));

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
            var result = await _pserv.DeleteAsync(id);

            _cs.RemoveData(_cs.GetData<string>($"payment_{id}"));

            return Ok(result);
        }


    }
}
