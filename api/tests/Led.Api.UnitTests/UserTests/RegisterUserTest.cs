using Led.Application.Users.Register;
using Led.Domain.Tenants.Repositories;
using Led.SharedKernal.Clock;
using Led.SharedKernal.UoW;
using NSubstitute;
using Shouldly;

namespace Led.Api.UnitTests.UserTests;

public class RegisterUserTest
{

    [Theory]
    [InlineData("", "valid", "valid", "valid@valid.org")]
    [InlineData("valid", "", "valid", "valid@valid.org")]
    [InlineData("valid", "valid", "", "valid@valid.org")]
    [InlineData("valid", "valid", "valid", "")]
    public async Task Handle_Should_ReturnFailure_WhenInputIsInvlid(string firstName, string lastName, string username, string email)
    {
        // Arrange
        var handler = GetHandler();
        var command = new RegisterUserCommand(firstName, lastName, username, email, Password: "");

        // Act
        var result = await handler.HandleAsync(command, TestContext.Current.CancellationToken);

        // Assert
        result.IsFailed.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_Should_AddUser()
    {
        // Arrange
        var handler = GetHandler();
        var command = new RegisterUserCommand("valid", "valid", "valid", "valid@valid.org", "");

        // Act
        var result = await handler.HandleAsync(command, TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBe(Guid.Empty);
    }

    private RegisterUserCommandHandler GetHandler(IUserRepository? userRepository = null, IDateTimeProvider? dateTimeProvider = null)
    {
        var uowMock = Substitute.For<IUnitOfWorkManager>();

        if (dateTimeProvider is null)
        {
            dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(DateTime.UtcNow);
        }

        userRepository ??= Substitute.For<IUserRepository>();

        return new RegisterUserCommandHandler(userRepository: userRepository, dateTimeProvider: dateTimeProvider, unitOfWorkManager: uowMock);
    }
}
