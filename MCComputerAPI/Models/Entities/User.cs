
using System.ComponentModel.DataAnnotations;

namespace MCComputerAPI.Models.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public required string FirstName { get; set; }

    [MaxLength(20)]
    public required string LastName { get; set; }

    [MaxLength(20)]
    public required string UserName { get; set; }

    [MaxLength(100)]
    public required string Password { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
}