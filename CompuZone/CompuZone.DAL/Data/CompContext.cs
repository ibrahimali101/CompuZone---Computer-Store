using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.DAL.Data
{
    public class CompContext : DbContext
    {
        DbSet<Product> Products;
        DbSet<CategoryDto> Categories;
        DbSet<ProductImageDto> ProductImages;
        DbSet<OrderItem> OrderItems;
        DbSet<Order> Orders;
        DbSet<Customer> Customers;
        DbSet<Payment> Payments;
        DbSet<Shipping> Shippings;


    }
}
