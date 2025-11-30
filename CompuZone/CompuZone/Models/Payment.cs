using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompUZone.Models;

[Index("OrderId", Name = "UQ__Payments__C3905BAEF47B07CA", IsUnique = true)]
public partial class Payment
{
    [Key]
    [Column("PaymentID")]
    public int PaymentId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal Amount { get; set; }

    [StringLength(50)]
    public string PaymentMethod { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime TransactionDate { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Payment")]
    public virtual Order Order { get; set; } = null!;
}
