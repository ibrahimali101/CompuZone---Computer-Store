using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompuZone.DAL.Entities.Configuration
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(a => a.CategoryID);

            builder.Property(a => a.CategoryName).HasMaxLength(70).IsRequired();

            builder.HasMany(a => a.Products)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
