using MCComputerAPI.Data.Interfaces;
using MCComputerAPI.Models.DTOs;
using MCComputerAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MCComputerAPI.Helpers;
using Serilog;

namespace MCComputerAPI;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUsersRepositories _userRepositories;

    public UserController(IUsersRepositories usersRepositories)
    {
        _userRepositories = usersRepositories;

    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from API!");
    }

    [HttpPost("add-user")]
    public IActionResult AddNewUser([FromBody] UserDTOs user)
    {

        if (user == null)
        {
            return BadRequest("User data is required.");
        }

        try
        {
            User userDetaisl = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password=  HashingHelper.HashPassword(user.Password)
            };

            // Map DTO to entity and initialize required properties
            bool isUserAdded = _userRepositories.SaveUserDetails(userDetaisl);
            
            if (isUserAdded)
            {
                return StatusCode(201, "User created successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while saving user details.");
            }
        }
        catch (Exception ex)
        {
            Log.Error("Exception in AddNewUser: " + ex);
            return StatusCode(500, "An unexpected error occurred.");
        }

    }

}
