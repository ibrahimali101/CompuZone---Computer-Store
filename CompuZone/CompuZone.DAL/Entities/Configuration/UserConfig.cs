using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompuZone.DAL.Entities.Configuration
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(u => u.PasswordHash)
                   .IsRequired();
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(u => u.DateJoined)
                   .HasDefaultValueSql("GETDATE()");
            builder.HasIndex(u => u.UserName)
                   .IsUnique();
            builder.HasIndex(u => u.Email)
                   .IsUnique();
        }
    }
}
