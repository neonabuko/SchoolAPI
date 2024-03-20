using Microsoft.AspNetCore.Mvc;

namespace WizardAPI.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        return Ok();
    }
}