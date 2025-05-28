

using System.ComponentModel.DataAnnotations;

namespace MCComputerAPI.Models.Entities;
public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    public required string Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }
}