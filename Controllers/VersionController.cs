using Microsoft.AspNetCore.Mvc;

namespace Todo.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VersionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetVersion()
    {
        return Ok("Version 1.0.2");
    }
}
