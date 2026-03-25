using FluentResults;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Users.Register;

public sealed record RegisterUserCommand(string FirstName, string LastName, string Username, string Email, string Password) : ICommand<Result<Guid>>;
