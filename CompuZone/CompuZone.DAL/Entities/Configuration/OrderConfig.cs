using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace CompuZone.DAL.Entities.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(a => a.OrderID);

            builder.Property(a => a.OrderDate)
                .IsRequired();

            builder.Property(a => a.TotalAmount).HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(a => a.Status).IsRequired();

            builder.HasOne(a => a.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(a => a.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Shipping)
                .WithOne(s => s.Order);

            builder.HasMany(a => a.OrderItems)
                .WithOne(a => a.Order);
        }
    }
}
