using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

[Index("ProductId", "CustomerId", Name = "UQ__Reviews__2E4620A7515F7E87", IsUnique = true)]
public partial class Review
{
    [Key]
    [Column("ReviewID")]
    public int ReviewId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [StringLength(500)]
    public string? ReviewText { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal Rating { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReviewDate { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Reviews")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Reviews")]
    public virtual ProductCatalog Product { get; set; } = null!;
}
