namespace Led.WebApi.Controllers.Users.Requests;

public sealed record RegisterUserRequest(string FirstName, string LastName, string Username, string Email, string Password);
