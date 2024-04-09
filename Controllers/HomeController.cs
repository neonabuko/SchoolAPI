using Microsoft.AspNetCore.Mvc;

namespace SchoolAPI.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Index() => Ok();
}