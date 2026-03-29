namespace Led.WebApi.Controllers.Devices.Requests;

public sealed record VerifyDeviceRequest(string IpAddress, string Username, string Password);
