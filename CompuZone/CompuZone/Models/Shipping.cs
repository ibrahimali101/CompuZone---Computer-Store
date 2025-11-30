using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

[Index("OrderId", Name = "UQ__Shipping__C3905BAE8AA1380E", IsUnique = true)]
public partial class Shipping
{
    [Key]
    [Column("ShippingID")]
    public int ShippingId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [StringLength(100)]
    public string? CarrierName { get; set; }

    [StringLength(50)]
    public string? TrackingNumber { get; set; }

    public short ShippingStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EstimatedDeliveryDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActualDeliveryDate { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Shipping")]
    public virtual Order Order { get; set; } = null!;
}
