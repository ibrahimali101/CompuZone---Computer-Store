using CompUZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CompUZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CompuZoneContext _context;

        public AuthController(CompuZoneContext context)
        {
            _context = context;
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<ActionResult<Customer>> Register(RegisterDto request)
        {
            // 1. اتأكد إن الإيميل مش موجود قبل كده
            if (await _context.Customers.AnyAsync(c => c.Email == request.Email))
            {
                return BadRequest("البريد الإلكتروني مستخدم بالفعل");
            }

            // 2. تشفير الباسورد (بسيط)
            var hashedPassword = HashPassword(request.Password);

            // 3. إنشاء العميل
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Username = request.Email, // مؤقتاً هنخلي اليوزر نيم هو الإيميل
                HashedPassword = hashedPassword,
                Address = "عنوان افتراضي", // ممكن نعدله بعدين
                Phone = "0000000000"
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم إنشاء الحساب بنجاح", customerId = customer.CustomerId });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto request)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == request.Email);

            if (customer == null)
            {
                return BadRequest("البريد الإلكتروني غير صحيح");
            }

            if (customer.HashedPassword != HashPassword(request.Password))
            {
                return BadRequest("كلمة المرور غير صحيحة");
            }

            // بنرجع بيانات العميل عشان الفرونت إند يحفظها
            return Ok(new
            {
                customerId = customer.CustomerId,
                name = customer.Name,
                email = customer.Email
            });
        }

        // دالة مساعدة لتشفير الباسورد
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }

    // كلاسات مساعدة لاستقبال البيانات (DTOs)
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}