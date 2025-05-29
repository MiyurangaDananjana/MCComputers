using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCComputerAPI.Models.Entities;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public double Stock { get; set; }

    public virtual ICollection<InvoiceItems> InvoiceItems { get; set; } = new List<InvoiceItems>();

}