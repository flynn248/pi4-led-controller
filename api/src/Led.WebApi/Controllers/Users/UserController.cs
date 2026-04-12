using Led.Application.Users.Register;
using Led.Application.Users.UpdateEmail;
using Led.SharedKernal.Errors;
using Led.WebApi.Controllers.Users.Requests;
using Led.WebApi.Extensions;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Controllers.Users;

public class UserController : BaseController
{
    private readonly ICommandMediator _commandMediator;

    public UserController(ICommandMediator commandMediator)
    {
        _commandMediator = commandMediator;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesErrorTypeResponse(ErrorType.Validation)]
    [ProducesErrorTypeResponse(ErrorType.Conflict)]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.FirstName, request.LastName, request.Username, request.Email, request.Password);

        var res = await _commandMediator.SendAsync(command, cancellationToken);

        return res.EvaluateResult(Ok);
    }

    [HttpPatch]
    [Route("{userId:guid}/email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesErrorTypeResponse(ErrorType.Validation)]
    [ProducesErrorTypeResponse(ErrorType.Conflict)]
    [ProducesErrorTypeResponse(ErrorType.NotFound)]
    public async Task<IActionResult> UpdateEmail(Guid userId, [FromBody] string email, CancellationToken cancellationToken)
    {
        var command = new UpdateEmailCommand(userId, email);

        var res = await _commandMediator.SendAsync(command, cancellationToken);

        return res.MatchResult(NoContent);
    }
}
