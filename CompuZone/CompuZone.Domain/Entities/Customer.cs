using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompuZone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

[Index("Username", Name = "UQ__Customer__536C85E4BEB87F6A", IsUnique = true)]
[Index("Email", Name = "UQ__Customer__A9D10534EBDAF9EA", IsUnique = true)]
public partial class Customer : BaseEntity
{
    [Key]
    [Column("CustomerID")]
    public int CustomerID { get; set; }

    [StringLength(100)]
    public string Full_name { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [StringLength(100)]
    public string Username { get; set; } = null!;

    [Column("hashedPassword")]
    [StringLength(100)]
    public string HashedPassword { get; set; } = null!;

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}