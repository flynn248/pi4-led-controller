using Led.Application.Users.Register;
using Led.Application.Users.UpdateEmail;
using Led.SharedKernal.Errors;
using Led.SharedKernal.FluentResult;
using Led.SharedKernal.UoW;
using Led.WebApi.Controllers.Users.Requests;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class UserController : ControllerBase
{
    private readonly ICommandMediator _commandMediator;

    public UserController(IUnitOfWorkManager unitOfWorkManager, ICommandMediator commandMediator)
    {
        _commandMediator = commandMediator;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.FirstName, request.LastName, request.UserName, request.Email, request.Password);

        var res = await _commandMediator.SendAsync(command, cancellationToken);

        if (res.IsFailed) // TODO: Handle more globally
        {
            var errorType = res.Errors[0].GetErrorType();

            return errorType switch
            {
                ErrorType.Validation => BadRequest(res.Errors[0].Message),
                _ => StatusCode(500, "An unexpected error occurred."),
            };
        }

        return NoContent();
    }

    //[HttpGet]
    //[Route("me")]
    //public IActionResult GetCurrentUser()
    //{
    //    using var uow = _unitOfWorkManager.Begin();
    //    uow.Dispose();

    //    return Ok();
    //}

    [HttpPatch]
    [Route("{userId:guid}/email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmail(Guid userId, [FromBody] string email, CancellationToken cancellationToken)
    {
        var command = new UpdateEmailCommand(userId, email);

        var res = await _commandMediator.SendAsync(command, cancellationToken);

        if (res.IsFailed) // TODO: Handle more globally
        {
            var errorType = res.Errors[0].GetErrorType();

            return errorType switch
            {
                ErrorType.NotFound => NotFound(res.Errors[0].Message),
                ErrorType.Validation => BadRequest(res.Errors[0].Message),
                _ => StatusCode(500, "An unexpected error occurred."),
            };
        }

        return NoContent();
    }
}
