using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompuZone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

[Table("ProductCatalog")]
public partial class ProductCatalog : NamedEntity
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("ProductCatalogs")]
    public virtual ProductCategory Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
