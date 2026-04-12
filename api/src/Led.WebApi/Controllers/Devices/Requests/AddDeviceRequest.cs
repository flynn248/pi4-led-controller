namespace Led.WebApi.Controllers.Devices.Requests;

public sealed record AddDeviceRequest(Guid TenantId, string Name, string IpAddress, string Username, string Password, string Description);
