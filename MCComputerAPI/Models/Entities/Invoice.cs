using System.ComponentModel.DataAnnotations;

namespace MCComputerAPI.Models.Entities;

public class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public decimal TotalAmmount { get; set; }

    public virtual ICollection<InvoiceItems> InvoiceItems { get; set; } = new List<InvoiceItems>();

}