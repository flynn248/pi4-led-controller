using AutoFixture;
using FluentResults;
using Led.Domain.EffectTypes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.EffectTypes.ValueObjects;

public class ParameterKeyTests
{
    private readonly Fixture _fixture;

    public ParameterKeyTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Fact]
    public void Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = ParameterKey.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { ParameterKeyErrors.InvalidLength(ParameterKey.MaxLength) }.AsReadOnly();

        // Act
        var res = ParameterKey.Create(invalidStringMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Should_ReturnValidationError_WithEmptyStringInput(string? input)
    {
        // Arrange
        var expectedErrors = new List<IError>() { ParameterKeyErrors.Empty }.AsReadOnly();

        // Act
        var res = ParameterKey.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData(" test valid input")]
    [InlineData(" test valid input ")]
    [InlineData("test valid input ")]
    public void Create_Should_RemoveWhitespaceFromEnds(string input)
    {
        // Arrange
        var expectedResult = input.Trim();

        // Act
        var res = ParameterKey.Create(input);

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
        var instance1 = ParameterKey.Create(input).Value;
        var instance2 = ParameterKey.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
