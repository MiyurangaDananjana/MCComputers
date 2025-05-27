using Microsoft.AspNetCore.Mvc;

namespace MCComputerAPI;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from API!");
    }

}
