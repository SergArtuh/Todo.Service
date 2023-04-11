using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Todo.Service.Model.User;
using Todo.Service.Services;



namespace Todo.Service.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SignupController : ControllerBase
{
    private SecurityService _securityService;

    public SignupController( SecurityService securityService)
    {
        _securityService = securityService;
    }

    

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Signup([FromBody] UserSignupDto signup)
    {
        var user = _securityService.CreateUser(new UserSignup() {Name = signup.UserName, Password = signup.UserPass, Email = signup.Email, Role = "User"});
        if(user == null) {
            return Conflict("User with such name already exists");
        }

        return Ok(_securityService.GenerateToken(user));
    }
}


