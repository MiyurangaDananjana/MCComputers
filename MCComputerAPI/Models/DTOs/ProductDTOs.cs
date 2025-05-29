
using System.ComponentModel.DataAnnotations;

namespace MCComputerAPI.Models.DTOs;

public class ProductDTOs
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
    public string Description { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Stock must be a non-negative value.")]
    public decimal Stock { get; set; }
}