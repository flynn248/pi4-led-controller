using AutoFixture;
using FluentResults;
using Led.Domain.EffectTypes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.Domain.EffectTypes.ValueObjects;

public class ParameterDescriptionTests
{
    private readonly Fixture _fixture;

    public ParameterDescriptionTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Fact]
    public void Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = ParameterDescription.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { ParameterDescriptionErrors.InvalidLength(ParameterDescription.MaxLength) }.AsReadOnly();

        // Act
        var res = ParameterDescription.Create(invalidStringMock);

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
        var res = ParameterDescription.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.ShouldBeEquivalentTo(ParameterDescription.Empty);
    }

    [Fact]
    public void Create_Should_RemoveWhitespaceFromEnds()
    {
        // Arrange
        const string input = " test valid input ";
        var expectedResult = input.Trim();

        // Act
        var res = EffectTypeName.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expectedResult);
    }

    [Fact]
    public void TwoObjects_WithSameValue_Should_BeEqual()
    {
        // Arrange
        const string input = "test valid input";

        // Act
        var instance1 = EffectTypeName.Create(input).Value;
        var instance2 = EffectTypeName.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
