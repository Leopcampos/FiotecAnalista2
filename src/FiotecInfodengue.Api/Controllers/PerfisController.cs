using Microsoft.AspNetCore.Mvc;

namespace FiotecInfodengue.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PerfisController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}