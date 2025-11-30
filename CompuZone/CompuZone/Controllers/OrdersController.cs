using CompUZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CompuZoneContext _context;

        public OrdersController(CompuZoneContext context)
        {
            _context = context;
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> PlaceOrder(CreateOrderDto request)
        {
            // 1. إنشاء الطلب الأساسي
            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = request.TotalAmount,
                Status = 1 // 1 = Pending (تحت المراجعة)
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // عشان ناخد الـ OrderId

            // 2. إضافة المنتجات للطلب
            foreach (var item in request.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                _context.OrderItems.Add(orderItem);
            }

            // 3. إضافة بيانات الشحن
            var shipping = new Shipping
            {
                OrderId = order.OrderId,
                CarrierName = "CompuZone Delivery",
                ShippingStatus = 1,
                EstimatedDeliveryDate = DateTime.Now.AddDays(3)
            };
            _context.Shippings.Add(shipping);

            // 4. إضافة بيانات الدفع
            var payment = new Payment
            {
                OrderId = order.OrderId,
                Amount = request.TotalAmount,
                PaymentMethod = "Credit Card", // أو حسب اللي جاي من الفرونت
                TransactionDate = DateTime.Now
            };
            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            return Ok(new { message = "تم تسجيل الطلب بنجاح", orderId = order.OrderId });
        }

        // GET: api/Orders/User/5
        // عشان تجيب طلبات مستخدم معين
        [HttpGet("user/{customerId}")]
        public async Task<ActionResult> GetUserOrders(int customerId)
        {
            var orders = await _context.Orders
                                       .Include(o => o.OrderItems)
                                       .ThenInclude(oi => oi.Product)
                                       .Where(o => o.CustomerId == customerId)
                                       .OrderByDescending(o => o.OrderDate)
                                       .ToListAsync();
            return Ok(orders);
        }
    }

    // DTOs لاستقبال شكل الطلب من الفرونت إند
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public string ShippingAddress { get; set; }
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}