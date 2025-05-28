using System.ComponentModel.DataAnnotations;

namespace MCComputerAPI.Models.Entities;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public double Stock { get; set; }

    public virtual ICollection<InvoiceItems> InvoiceItems { get; set; } = new List<InvoiceItems>();

}