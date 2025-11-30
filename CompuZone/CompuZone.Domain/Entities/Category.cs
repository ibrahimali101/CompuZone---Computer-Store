using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompuZone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

[Table("ProductCategory")]
[Index("CategoryName", Name = "UQ__ProductC__8517B2E054CE9FA8", IsUnique = true)]
public partial class Category : NamedEntity
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [StringLength(100)]
    public string CategoryName { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
