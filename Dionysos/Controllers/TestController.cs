using Microsoft.AspNetCore.Mvc;

namespace Dionysos.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController
{
    [HttpGet("Lara")]
    public string Test()
    {
        return "Hello World";
    }
}

