using System.ComponentModel.DataAnnotations;

namespace MCComputerAPI.Models.DTOs;

public class InvoiceItemsDTOs
{
    public int? InvoiceItemsId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Price must be non-negative.")]
    public decimal Price { get; set; }
}