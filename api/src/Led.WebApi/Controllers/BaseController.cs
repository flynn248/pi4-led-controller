using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public abstract class BaseController : ControllerBase
{
}
