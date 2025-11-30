using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

public partial class CompuZoneContext : DbContext
{
    public CompuZoneContext()
    {
    }

    public CompuZoneContext(DbContextOptions<CompuZoneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<ProductCatalog> ProductCatalogs { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=CompuZone;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B86433097A");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF85A9A203");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Customer__48CFD27E");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__OrderIte__08D097C125E63747");

            entity.Property(e => e.TotalItemsPrice).HasComputedColumnSql("([Quantity]*[Price])", true);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__4D94879B");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Produ__4E88ABD4");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A5890A9E6D7");

            entity.Property(e => e.TransactionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithOne(p => p.Payment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__OrderI__5441852A");
        });

        modelBuilder.Entity<ProductCatalog>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__ProductC__B40CC6ED81B37ABC");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCatalogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductCa__Categ__403A8C7D");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__ProductC__19093A2B5C1DB983");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ProductI__7516F4ECC6B89C03");

            entity.Property(e => e.ImageOrder).HasDefaultValue((short)1);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__Produ__440B1D61");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE2A7170BB");

            entity.Property(e => e.ReviewDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Custome__5EBF139D");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Product__5DCAEF64");
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.ShippingId).HasName("PK__Shipping__5FACD46094D41921");

            entity.HasOne(d => d.Order).WithOne(p => p.Shipping)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shippings__Order__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
