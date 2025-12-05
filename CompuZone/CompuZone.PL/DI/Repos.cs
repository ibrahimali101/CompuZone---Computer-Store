using CompuZone.BLL.Interfaces;
using CompuZone.DAL.Repository.Implementation;
using CompuZone.DAL.Repository.Interfaces;

namespace CompuZone.PL.DI
{
    public static class Repos
    {
        public static IServiceCollection AddRepositores(this IServiceCollection Services)
        {
            Services.AddScoped<IProductRepo, ProductRepo>();
            Services.AddScoped<ICategoryRepo, CategoryRepo>();
            Services.AddScoped<ICustomerRepo, CustomerRepo>();
            Services.AddScoped<IOrderRepo, OrderRepo>();
            Services.AddScoped<IPaymentRepo, PaymentRepo>();
            Services.AddScoped<IShippingRepo, ShippingRepo>();
            Services.AddScoped<IProductImageRepo, ProductImageRepo>();
            Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            return Services;
        }
    }
}
