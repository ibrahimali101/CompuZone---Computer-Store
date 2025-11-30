using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

public partial class ProductImage
{
    [Key]
    [Column("ImageID")]
    public int ImageId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("ImageURL")]
    [StringLength(400)]
    public string ImageUrl { get; set; } = null!;

    public short ImageOrder { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductImages")]
    public virtual ProductCatalog Product { get; set; } = null!;
}
