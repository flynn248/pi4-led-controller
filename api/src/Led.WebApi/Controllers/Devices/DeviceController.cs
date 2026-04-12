using Led.Application.Devices.AddDevice;
using Led.Application.Devices.VerifyConnection;
using Led.SharedKernal.Errors;
using Led.WebApi.Controllers.Devices.Requests;
using Led.WebApi.Extensions;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Controllers.Devices;

public class DeviceController : BaseController
{
    private readonly ICommandMediator _commandMediator;

    public DeviceController(ICommandMediator commandMediator)
    {
        _commandMediator = commandMediator;
    }

    [HttpPost]
    [Route("verify-connection")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesErrorTypeResponse(ErrorType.Failure)]
    [ProducesErrorTypeResponse(ErrorType.Validation)]
    public async Task<IActionResult> VerifyDeviceConnection(VerifyDeviceRequest request, CancellationToken cancellationToken)
    {
        var command = new VerifyDeviceConnectionCommand(request.IpAddress, request.Username, request.Password);

        var res = await _commandMediator.SendAsync(command, cancellationToken);

        return res.MatchResult(Ok);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddDevice(AddDeviceRequest request, CancellationToken cancellationToken)
    {
        var command = new AddDeviceCommand(request.TenantId, request.Name, request.IpAddress, request.Username, request.Password, request.Description);

        var res = await _commandMediator.SendAsync(command, cancellationToken);

        return res.MatchResult(NoContent);
    }
}
