namespace Led.WebApi.Controllers.Users.Requests;

public sealed record RegisterUserRequest(string FirstName, string LastName, string UserName, string Email, string Password);
