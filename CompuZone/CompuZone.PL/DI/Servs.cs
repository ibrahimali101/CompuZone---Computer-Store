using CompuZone.BLL.AuthStuffs;
using CompuZone.BLL.Services.Implementation;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Repository.Implementation;
using CompuZone.DAL.Repository.Interfaces;

namespace CompuZone.PL.DI
{
    public static class Servs
    {
        public static IServiceCollection AddServices(this IServiceCollection Services)
        {
            Services.AddScoped<IAuthService, AuthService>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<ICustomerService, CustomerService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IOrderItemService, OrderItemService>();
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<IShippingService, ShippingService>();
            Services.AddScoped<IProductImageService, ProductImageService>();
            Services.AddScoped<IJwtService, JwtService>();
            return Services;
        }
    }
}
