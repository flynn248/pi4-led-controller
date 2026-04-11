using AutoFixture;
using FluentResults;
using Led.Domain.Schedules.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.Schedules.ValueObjects;

public class CronExpressionTests
{
    private readonly Fixture _fixture;

    public CronExpressionTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Should_ReturnEmpty(string? input)
    {
        // Arrange
        var expected = CronExpression.Empty;

        // Act
        var res = CronExpression.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = CronExpression.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { CronExpressionErrors.InvalidLength(CronExpression.MaxLength) }.AsReadOnly();

        // Act
        var res = CronExpression.Create(invalidStringMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData("test invalid cron expression")]
    [InlineData("* * * * * *")]
    [InlineData("0 0 12 * */2 Mon")]
    public void Create_Should_ReturnInvalidError(string input)
    {
        // Arrange
        var expectedErrors = new List<IError>() { CronExpressionErrors.Invalid }.AsReadOnly();

        // Act
        var res = CronExpression.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData("* * * * *")]
    [InlineData(" * * * * * ")]
    [InlineData("0 12 * */2 Mon")]
    public void Create_Should_BeValid(string input)
    {
        // Arrange
        var expectedValue = input.Trim();

        // Act
        var res = CronExpression.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void TwoObjects_WithSameValue_Should_BeEqual()
    {
        // Arrange
        const string input = "* * * * *";

        // Act
        var instance1 = CronExpression.Create(input).Value;
        var instance2 = CronExpression.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
