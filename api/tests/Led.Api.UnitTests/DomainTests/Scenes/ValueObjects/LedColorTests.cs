using FluentResults;
using Led.Domain.Scenes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.Scenes.ValueObjects;

public class LedColorTests
{
    [Theory]
    [InlineData(LedColor.MinValue - 1)]
    [InlineData(LedColor.MaxValue + 1)]
    public void Create_Should_ReturnInvalidValueError(short input)
    {
        // Arrange
        var expectedErrors = new List<IError>() { LedColorErrors.InvalidValue(LedColor.MinValue, LedColor.MaxValue) }.AsReadOnly();

        // Act
        var res = LedColor.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData(8)]
    [InlineData(67)]
    [InlineData(204)]
    [InlineData(LedColor.MinValue)]
    [InlineData(LedColor.MaxValue)]
    public void Create_Should_BeValid(short input)
    {
        // Arrange

        // Act
        var res = LedColor.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(input);
    }

    [Theory]
    [InlineData(LedColor.MinValue)]
    [InlineData(LedColor.MaxValue)]
    public void TwoObjects_WithSameValue_Should_BeEqual(short input)
    {
        // Arrange

        // Act
        var instance1 = LedColor.Create(input).Value;
        var instance2 = LedColor.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
