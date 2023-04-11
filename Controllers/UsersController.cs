using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Todo.Service.Services;
using Todo.Service.Model.User;


namespace Todo.Service.Controllers;

[ApiController]
[Authorize]
[Route("api/admin/[controller]")]
public class UsersController : ControllerBase
{
    private SecurityService _securityService;

    public UsersController(SecurityService securityService)
    {
        _securityService = securityService;
    }

    

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult Login()
    {
        var users = _securityService.GetUsers();
        return Ok(users.Select(userModel=>userModel.AsDto()));
    }
}
