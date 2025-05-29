using System.ComponentModel.DataAnnotations;
using MCComputerAPI.Models.DTOs;

namespace MCComputerAPI.Models.DTOs;

public class InvoiceDTOs
{
    public int? InvoiceId { get; set; }

    [Required(ErrorMessage = "CustomerId is required.")]
    public int CustomerId { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "TotalAmount must be non-negative.")]
    public decimal TotalAmmount { get; set; }

    public List<InvoiceItemsDTOs> InvoiceItems { get; set; } = new List<InvoiceItemsDTOs>();
}