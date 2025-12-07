using CompuZone.BLL.DTOs.Customer;
using CompuZone.BLL.DTOs.Response;
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
        private readonly ICacheService _cs;
        public CustomerController(ICustomerService cserv, ICacheService cs)
        {
            _cserv = cserv;
            _cs = cs;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = _cs.GetData<ResponseDto<List<ResCustomerDto>>>("customers");

            if (res != null)
            {
                return Ok(res);
            }

            var result = await _cserv.GetAllAsync();

            _cs.SetData("customers", result, DateTimeOffset.Now.AddMinutes(5));

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var res = _cs.GetData<ResponseDto<ResCustomerDto>>($"customer_{id}");
            if (res != null)
            {
                return Ok(res);
            }
            var result = await _cserv.GetByIdAsync(id);
            _cs.SetData($"customer_{id}", result, DateTimeOffset.Now.AddMinutes(5));
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] ReqCustomerDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _cserv.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReqCustomerDto dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized("You are not an Admin."); // Returns 401
            }
            var result = await _cserv.UpdateAsync(id, dto);
            _cs.RemoveData(_cs.GetData<string>($"customer_{id}"));
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
            var result = await _cserv.DeleteAsync(id);
            _cs.RemoveData(_cs.GetData<string>($"customer_{id}"));
            return Ok(result);
        }
    }
}
