using FluentResults;
using Led.Application.Users.UpdateEmail;
using Led.Domain.Tenants.EntityErrors;
using Led.Domain.Tenants.Repositories;
using Led.Domain.Tenants.ValueObjects;
using Led.SharedKernal.Clock;
using Led.SharedKernal.UoW;
using NSubstitute;
using Shouldly;

namespace Led.Api.UnitTests.UserTests;

public class UpdateEmailTest
{
    [Fact]
    public async Task HandleAsync_Should_ReturnFailure_WhenUserIdDoesNotExist()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;
        var validEmailMock = "valid@valid.com";
        var commandMock = new UpdateEmailCommand(Guid.Empty, validEmailMock);

        var handler = GetHandler();

        var expectedErrors = new List<IError>() { UserError.NotFound }.AsReadOnly();

        // Act
        var res = await handler.HandleAsync(commandMock, cancellationToken);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Fact]
    public async Task HandleAsync_Should_ReturnFailure_WhenEmailIsDuplicate()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        var duplicateEmailMock = "duplicate@dupe.com";
        var userMock = UserData.Create(DateTime.UtcNow);
        var commandMock = new UpdateEmailCommand(userMock.Id, duplicateEmailMock);

        var userRepoMock = Substitute.For<IUserRepository>();
        userRepoMock.GetById(commandMock.UserId, cancellationToken)
            .Returns(userMock);

        userRepoMock.IsDuplicateEmail(commandMock.UserId, duplicateEmailMock, cancellationToken)
            .Returns(true);

        var handler = GetHandler(userRepository: userRepoMock);

        var expectedErrors = new List<IError>() { UserError.EmailExists }.AsReadOnly();

        // Act
        var res = await handler.HandleAsync(commandMock, cancellationToken);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
        await userRepoMock.Received(1).IsDuplicateEmail(commandMock.UserId, duplicateEmailMock, cancellationToken);
    }

    [Fact]
    public async Task HandleAsync_Should_ReturnFailure_WhenEmailIsInvalid()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        var invalidEmailMock = "fakeEmail.com";
        var userMock = UserData.Create(DateTime.UtcNow);
        var commandMock = new UpdateEmailCommand(userMock.Id, invalidEmailMock);

        var userRepoMock = Substitute.For<IUserRepository>();
        userRepoMock.GetById(commandMock.UserId, cancellationToken)
            .Returns(userMock);

        userRepoMock.IsDuplicateEmail(commandMock.UserId, invalidEmailMock, cancellationToken)
            .Returns(false);

        var handler = GetHandler(userRepository: userRepoMock);

        // Act
        var res = await handler.HandleAsync(commandMock, cancellationToken);

        // Assert
        res.IsFailed.ShouldBeTrue();
    }

    [Fact]
    public async Task HandleAsync_Should_UpdateEmail()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        const string validEmailMock = "validEmailTest@valid.com";
        var userMock = UserData.Create(DateTime.UtcNow);
        var commandMock = new UpdateEmailCommand(userMock.Id, validEmailMock);

        var userRepoMock = Substitute.For<IUserRepository>();
        userRepoMock.GetById(commandMock.UserId, cancellationToken)
            .Returns(userMock);

        userRepoMock.IsDuplicateEmail(commandMock.UserId, validEmailMock, cancellationToken)
            .Returns(false);

        var uowManagerMock = UnitOfWorkMockHelper.GetUnitOfWorkManagerMock(out var unitOfWorkMock);

        var handler = GetHandler(userRepository: userRepoMock, unitOfWorkManager: uowManagerMock);

        // Act
        var res = await handler.HandleAsync(commandMock, cancellationToken);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        userMock.Email.ShouldBe(Email.Create(validEmailMock).Value);
        await unitOfWorkMock.Received(1).SaveChanges(cancellationToken);
    }

    private UpdateEmailCommandHandler GetHandler(IUserRepository? userRepository = null, IDateTimeProvider? dateTimeProvider = null, IUnitOfWorkManager? unitOfWorkManager = null)
    {
        unitOfWorkManager ??= Substitute.For<IUnitOfWorkManager>();

        if (dateTimeProvider is null)
        {
            dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(DateTime.UtcNow);
        }

        userRepository ??= Substitute.For<IUserRepository>();

        return new UpdateEmailCommandHandler(userRepository: userRepository, dateTimeProvider: dateTimeProvider, unitOfWorkManager: unitOfWorkManager);
    }
}
