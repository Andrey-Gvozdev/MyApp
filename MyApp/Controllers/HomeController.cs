using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class HomeController : ControllerBase
{
    public HomeController()
    {
    }
}