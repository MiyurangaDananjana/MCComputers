using System.ComponentModel.DataAnnotations;

namespace MCComputerAPI.Models.DTOs;

public class UserDTOs
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}