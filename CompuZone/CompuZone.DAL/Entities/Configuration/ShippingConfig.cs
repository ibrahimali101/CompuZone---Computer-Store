using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompuZone.DAL.Entities.Configuration
{
    public class ShippingConfig : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.HasKey(a => a.ShippingID);

            builder.Property(a => a.Address).IsRequired();

            builder.HasOne(a => a.Order)
                   .WithOne(o => o.Shipping)
                   .HasForeignKey<Shipping>(a => a.OrderID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.CustomerID).IsRequired();

            builder.Property(a => a.TrackingNumber).IsRequired(false);

            builder.Property(a => a.ShippingStatus).HasDefaultValue("Pending");

            builder.Property(a => a.EstimatedDeliveryDate).IsRequired(false);

            builder.Property(a => a.ActualDeliveryDate).IsRequired(false);


        }
    }
}
