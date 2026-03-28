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
    public async Task HandleAsync_Should_ReturnFailure_WhenInputIsInvlid(string firstName, string lastName, string username, string email)
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;
        var handler = GetHandler();
        var command = new RegisterUserCommand(firstName, lastName, username, email, Password: "");

        // Act
        var result = await handler.HandleAsync(command, cancellationToken);

        // Assert
        result.IsFailed.ShouldBeTrue();
    }

    [Fact]
    public async Task HandleAsync_Should_AddUser()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;
        var unitOfWorkManagerMock = UnitOfWorkMockHelper.GetUnitOfWorkManagerMock(out var unitOfWorkMock);
        var handler = GetHandler(unitOfWorkManager: unitOfWorkManagerMock);
        var command = new RegisterUserCommand("valid", "valid", "valid", "valid@valid.org", "");

        // Act
        var result = await handler.HandleAsync(command, cancellationToken);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBe(Guid.Empty);
        await unitOfWorkMock.Received(1).SaveChanges(cancellationToken);

    }

    private RegisterUserCommandHandler GetHandler(IUserRepository? userRepository = null, IDateTimeProvider? dateTimeProvider = null, IUnitOfWorkManager? unitOfWorkManager = null)
    {
        unitOfWorkManager ??= Substitute.For<IUnitOfWorkManager>();

        if (dateTimeProvider is null)
        {
            dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(DateTime.UtcNow);
        }

        userRepository ??= Substitute.For<IUserRepository>();

        return new RegisterUserCommandHandler(userRepository: userRepository, dateTimeProvider: dateTimeProvider, unitOfWorkManager: unitOfWorkManager);
    }
}
