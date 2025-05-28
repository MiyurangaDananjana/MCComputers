
namespace MCComputerAPI.Models.DTOs;
public class JwtSettingsDTOs
{
    public required string ValidIssuer { get; set; }
    public required string ValidAudience { get; set; }
    public required string SecretKey { get; set; }
}