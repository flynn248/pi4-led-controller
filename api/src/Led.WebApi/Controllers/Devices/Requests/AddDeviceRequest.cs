namespace Led.WebApi.Controllers.Devices.Requests;

public sealed record AddDeviceRequest(string Name, string IpAddress, string SerialNumber, string Description);
