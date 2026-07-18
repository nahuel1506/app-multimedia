using Api.Modules.Identity.DTOs;
using Api.Modules.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Identity.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public ActionResult<UserResponse> GetById(Guid id)
    {
        var user = userService.GetById(id);

        if (user is null)
            return NotFound();

        return Ok(user);
    }
}
