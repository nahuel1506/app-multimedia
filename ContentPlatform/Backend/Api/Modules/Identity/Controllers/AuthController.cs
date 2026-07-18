using Api.Modules.Identity.DTOs;
using Api.Modules.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Identity.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("signup")]
    public ActionResult<AuthResponse> SignUp([FromBody] SignUpRequest request)
    {
        try
        {
            return Ok(authService.SignUp(request));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpPost("login")]
    public ActionResult<AuthResponse> Login([FromBody] LoginRequest request)
    {
        try
        {
            return Ok(authService.Login(request));
        }
        catch (InvalidOperationException exception)
        {
            return Unauthorized(new { message = exception.Message });
        }
    }

    [HttpDelete("logout")]
    public IActionResult Logout([FromHeader] Guid sessionId)
    {
        return authService.Logout(sessionId)
            ? NoContent()
            : NotFound();
    }
}
