using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompuZone.DAL.Entities.Configuration
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(a => a.PaymentID);

            builder.Property(a => a.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.Amount).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(a => a.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(a => a.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.TransactionDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
