using AutoFixture;
using FluentResults;
using Led.Domain.EffectTypes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.Domain.EffectTypes.ValueObjects;

public class EffectTypeNameTests
{
    private readonly Fixture _fixture;

    public EffectTypeNameTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Fact]
    public void Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = EffectTypeName.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { EffectTypeNameErrors.InvalidLength(EffectTypeName.MaxLength) }.AsReadOnly();

        // Act
        var res = EffectTypeName.Create(invalidStringMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Should_ReturnValidationError_WithEmptyStringInput(string input)
    {
        // Arrange
        var expectedErrors = new List<IError>() { EffectTypeNameErrors.Empty }.AsReadOnly();

        // Act
        var res = EffectTypeName.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
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
