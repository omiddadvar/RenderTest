using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RenderTest.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OmidController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Omid's Test Passed :D !!!");
    }
}
