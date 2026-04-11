using AutoFixture;
using FluentResults;
using Led.Domain.EffectTypes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.Domain.EffectTypes.ValueObjects;

public class EffectTypeDescriptionTests
{
    private readonly Fixture _fixture;

    public EffectTypeDescriptionTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Fact]
    public void Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = EffectTypeDescription.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { EffectTypeDescriptionErrors.InvalidLength(EffectTypeDescription.MaxLength) }.AsReadOnly();

        // Act
        var res = EffectTypeDescription.Create(invalidStringMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Should_ReturnEmpty(string? input)
    {
        // Arrange

        // Act
        var res = EffectTypeDescription.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.ShouldBeEquivalentTo(EffectTypeDescription.Empty);
    }

    [Fact]
    public void Create_Should_RemoveWhitespaceFromEnds()
    {
        // Arrange
        const string input = " test valid input ";
        var expectedResult = input.Trim();

        // Act
        var res = EffectTypeDescription.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData("")]
    [InlineData("test valid input")]
    public void TwoObjects_WithSameValue_Should_BeEqual(string input)
    {
        // Arrange

        // Act
        var instance1 = EffectTypeDescription.Create(input).Value;
        var instance2 = EffectTypeDescription.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
