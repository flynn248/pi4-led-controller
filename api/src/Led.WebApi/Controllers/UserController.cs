using Led.SharedKernal.UoW;
using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public UserController(IUnitOfWorkManager unitOfWorkManager)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    [HttpGet]
    [Route("me")]
    public IActionResult GetCurrentUser()
    {
        using var uow = _unitOfWorkManager.Begin();
        uow.Dispose();

        return Ok();
    }
}
