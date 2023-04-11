using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using Todo.Service.Model.User;
using Todo.Service.Services;


namespace Todo.Service.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private SecurityService _securityService;

    public LoginController(SecurityService securityService)
    {
        _securityService = securityService;
    }

    

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login([FromBody] UserLoginDto login)
    {
        var user = _securityService.GetUser(new UserLogin() {Name = login.UserName, Password = login.UserPass});
        if(user == null) {
            return NotFound("User not found");
        }

        return Ok(_securityService.GenerateToken(user));
    }
}
