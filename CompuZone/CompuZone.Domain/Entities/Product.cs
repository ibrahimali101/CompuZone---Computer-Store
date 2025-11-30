using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompuZone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

[Table("ProductCatalog")]
public partial class Product : NamedEntity
{
    [Column(TypeName = "smallmoney")]
    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
