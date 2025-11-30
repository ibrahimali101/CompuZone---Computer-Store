using System;
using System.Collections.Generic;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using CompuZone.Infrastructure;
namespace CompUZone.Models;

public partial class CompuZoneContext : DbContext
{
    private readonly ICurrentUserService _currentUser;

    public CompuZoneContext()
    {
    }
    public CompuZoneContext(DbContextOptions<CompuZoneContext> options, ICurrentUserService currentUser
            ) :
            base(options)
    {
        _currentUser = currentUser;
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=CompuZone;Trusted_Connection=True;TrustServerCertificate=True;");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entiries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entiries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedById = _currentUser.UserId;
                    entry.Entity.CreatedByName = _currentUser.UserName;
                    entry.Entity.CreatedDateTime = DateTime.UtcNow;

                    entry.Entity.ModifiedById = _currentUser.UserId;
                    entry.Entity.ModifiedByName = _currentUser.UserName;
                    entry.Entity.ModifiedDateTime = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedById = _currentUser.UserId;
                    entry.Entity.ModifiedByName = _currentUser.UserName;
                    entry.Entity.ModifiedDateTime = DateTime.UtcNow;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IArchived = true;
                    entry.Entity.ArchivedById = _currentUser.UserId;
                    entry.Entity.ArchivedByName = _currentUser.UserName;
                    entry.Entity.ArchivedDateTime = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
