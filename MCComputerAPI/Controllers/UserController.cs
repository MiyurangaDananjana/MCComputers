using MCComputerAPI.Models.DTOs;
using MCComputerAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MCComputerAPI.Helpers;
using Serilog;
using MCComputerAPI.Repositories.Data.Interfaces;

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
                Password = HashingHelper.HashPassword(user.Password)
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

    [HttpPut("update-user")]
    public ActionResult<User> UpdateUser(UserDTOs userDTOs)
    {
        if (userDTOs == null)
        {
            return BadRequest("User data is required.");
        }

        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if user exists
            var existingUser = _userRepositories.GetUserByUsername(userDTOs.UserName);

            if (existingUser == null)
            {
                return NotFound($"User with username '{userDTOs.UserName}' not found.");
            }

            // Update user details
            existingUser.FirstName = userDTOs.FirstName;
            existingUser.LastName = userDTOs.LastName;

            // Only update the password if it's different
            string hashedNewPassword = HashingHelper.HashPassword(userDTOs.Password);
            if (existingUser.Password != hashedNewPassword)
            {
                existingUser.Password = hashedNewPassword;
            }

            var updatedUser = _userRepositories.UpdateUserDetails(existingUser);

            if (updatedUser == null)
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }

            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            // Log exception here if needed
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpDelete("delete-user/{id}")]
    public IActionResult DeleteUser(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid user ID.");
        }

        try
        {
            var existingUser = _userRepositories.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            bool deleteSuccess = _userRepositories.DeleteUser(id);

            if (!deleteSuccess)
            {
                return StatusCode(500, "An error occurred while deleting the user.");
            }

            return Ok($"User with ID {id} has been successfully deleted.");
        }
        catch (Exception ex)
        {
            // Optional: log exception
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
