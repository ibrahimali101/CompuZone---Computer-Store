using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompuZone.DAL.Entities.Configuration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(a => a.CustomerID);

            builder.Property(a => a.DateOfBirth)
                .IsRequired();
            builder.Property(a => a.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Email) // regex
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Phone) // regex
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(a => a.Address)
                .IsRequired();
        }
    }
}
