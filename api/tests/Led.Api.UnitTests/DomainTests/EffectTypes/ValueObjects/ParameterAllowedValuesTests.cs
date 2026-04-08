using System.Text.Json;
using AutoFixture;
using FluentResults;
using Led.Domain.EffectTypes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.EffectTypes.ValueObjects;

public class ParameterAllowedValuesTests
{
    private readonly Fixture _fixture;

    public ParameterAllowedValuesTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Fact]
    public void Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = ParameterAllowedValues.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var invalidJsonMock = JsonSerializer.Serialize(new
        {
            value = invalidStringMock
        });

        // Act
        var res = ParameterAllowedValues.Create(invalidJsonMock);

        // Assert
        var expectedErrors = new List<IError>() { ParameterAllowedValuesErrors.InvalidLength(ParameterAllowedValues.MaxLength) }.AsReadOnly();

        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("[]")]
    [InlineData("{}")]
    public void Create_Should_ReturnEmpty(string? input)
    {
        // Arrange

        // Act
        var res = ParameterAllowedValues.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.ShouldBeEquivalentTo(ParameterAllowedValues.Empty);
    }

    [Theory]
    [InlineData("{\"valid\": \"valid\"}")]
    [InlineData("[\"testValid\"]")]
    [InlineData("[\"testValid\", \"testValid2\"]")]
    [InlineData(" [\"testValid\", \"testValidSpace\"] ")]
    public void Create_Should_BeValidInput(string input)
    {
        // Arrange
        var expectedResult = input.Trim();

        // Act
        var res = ParameterAllowedValues.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData("not json")]
    [InlineData("{\"not\": \"json}")]
    [InlineData("[\"testValid\",]")]
    [InlineData("[\"testValid\", testValid2\"]")]
    public void Create_ShouldNot_BeValidJson(string input)
    {
        // Arrange

        // Act
        var res = ParameterAllowedValues.Create(input);

        // Assert
        var expectedErrors = new List<IError>() { ParameterAllowedValuesErrors.InvalidFormat }.AsReadOnly();

        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Fact]
    public void TwoObjects_WithSameValue_Should_BeEqual()
    {
        // Arrange
        var input = JsonSerializer.Serialize(
            new
            {
                value = "test valid input"
            });

        // Act
        var instance1 = ParameterAllowedValues.Create(input).Value;
        var instance2 = ParameterAllowedValues.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
