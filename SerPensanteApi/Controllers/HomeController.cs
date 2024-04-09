using Microsoft.AspNetCore.Mvc;

namespace SerPensanteApi.Controllers;


[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok();
    }
}