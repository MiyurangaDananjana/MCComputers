using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MCComputerAPI.Models.Entities;
using MCComputerAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MCComputerAPI;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTOs model)
    {
        // Fake validation
        if (model.Username == "admin" && model.Password == "password")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("your_super_secret_key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, model.Username)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "your_issuer",
                Audience = "your_audience",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

        return Unauthorized();
    }
}
