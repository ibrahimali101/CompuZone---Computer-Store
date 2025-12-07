using CompuZone.BLL.DTOs.Customer;
using CompuZone.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompuZone.PL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _cserv;
        public CustomerController(ICustomerService cserv)
        {
            _cserv = cserv;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _cserv.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _cserv.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReqCustomerDto dto)
        {
            var result = await _cserv.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqCustomerDto dto)
        {
            var result = await _cserv.UpdateAsync(id, dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _cserv.DeleteAsync(id);
            return Ok(result);
        }
    }
}
