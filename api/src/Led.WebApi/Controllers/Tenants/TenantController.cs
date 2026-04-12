using Led.Application.Tenants.AddTenant;
using Led.WebApi.Controllers.Tenants.Requests;
using Led.WebApi.Extensions;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Controllers.Tenants;

public class TenantController : BaseController
{
    private readonly ICommandMediator _commandMediator;

    public TenantController(ICommandMediator commandMediator)
    {
        _commandMediator = commandMediator;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTenant(CreateTenantRequest request, CancellationToken cancellationToken)
    {
        var command = new AddTenantCommand(request.UserId, request.Name);

        var res = await _commandMediator.SendAsync(command, cancellationToken);

        return res.EvaluateResult(Ok);
    }
}
