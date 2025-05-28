using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MCComputerAPI.Models.Entities;
using MCComputerAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using MCComputerAPI.Helpers;
using MCComputerAPI.Data.Interfaces;

namespace MCComputerAPI;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly JwtSettingsHelper _jwtHelper;

    private readonly IUsersRepositories _usersRepositories;

    public LoginController(JwtSettingsHelper jwtHelper, IUsersRepositories usersRepositories)
    {
        _jwtHelper = jwtHelper;
        _usersRepositories = usersRepositories;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTOs model)
    {
        if (_usersRepositories.IsUserValid(model.Username, HashingHelper.HashPassword(model.Password)))
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.Username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtHelper.ValidIssuer,
                Audience = _jwtHelper.ValidAudience,
                SigningCredentials = new SigningCredentials(_jwtHelper.IssuerSigningKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }

        return Unauthorized();
    }
}
