using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCComputerAPI.Models.Entities;

public class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer? Customer { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

   [Column(TypeName = "decimal(18,4)")]
    public decimal TotalAmmount { get; set; }

    public virtual ICollection<InvoiceItems> InvoiceItems { get; set; } = new List<InvoiceItems>();

}