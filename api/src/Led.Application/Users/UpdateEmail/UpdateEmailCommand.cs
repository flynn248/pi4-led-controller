using FluentResults;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Users.UpdateEmail;

public sealed record UpdateEmailCommand(Guid UserId, string NewEmail) : ICommand<Result>;
