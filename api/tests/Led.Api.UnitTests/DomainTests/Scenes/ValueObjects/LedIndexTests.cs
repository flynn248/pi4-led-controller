using FluentResults;
using Led.Domain.Scenes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.Scenes.ValueObjects;

public class LedIndexTests
{
    [Theory]
    [InlineData(LedIndex.MinValue - 1)]
    public void Create_Should_ReturnInvalidValueError(short input)
    {
        // Arrange
        var expectedErrors = new List<IError>() { LedIndexErrors.InvalidValue(LedIndex.MinValue) }.AsReadOnly();

        // Act
        var res = LedIndex.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData(LedIndex.MinValue)]
    [InlineData(LedIndex.MinValue + 10)]
    [InlineData(short.MaxValue)]
    public void Create_Should_BeValid(short input)
    {
        // Arrange

        // Act
        var res = LedIndex.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(input);
    }

    [Theory]
    [InlineData(LedIndex.MinValue)]
    [InlineData(LedIndex.MinValue + 10)]
    [InlineData(short.MaxValue)]
    public void TwoObjects_WithSameValue_Should_BeEqual(short input)
    {
        // Arrange

        // Act
        var instance1 = LedIndex.Create(input).Value;
        var instance2 = LedIndex.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
