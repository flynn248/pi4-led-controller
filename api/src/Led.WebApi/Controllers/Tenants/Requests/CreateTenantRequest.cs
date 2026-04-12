namespace Led.WebApi.Controllers.Tenants.Requests;

public sealed record CreateTenantRequest(Guid UserId, string Name);
