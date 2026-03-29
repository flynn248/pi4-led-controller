using Led.Application.Devices.VerifyConnection;
using Led.SharedKernal.Errors;
using Led.SharedKernal.FluentResult;
using Led.WebApi.Controllers.Devices.Requests;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Controllers.Devices;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class DeviceController : ControllerBase
{
    private readonly ICommandMediator _commandMediator;

    public DeviceController(ICommandMediator commandMediator)
    {
        _commandMediator = commandMediator;
    }

    [HttpPost]
    [Route("verify-connection")]
    public async Task<IActionResult> VerifyDeviceConnection(VerifyDeviceRequest request, CancellationToken cancellationToken)
    {
        var command = new VerifyDeviceConnectionCommand(request.IpAddress, request.Username, request.Password);

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

        return Ok(res.Value);
    }
}
