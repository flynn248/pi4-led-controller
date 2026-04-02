using AutoFixture;
using FluentResults;
using Led.Domain.EffectTypes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests;

public class EffectTypesTests
{
    private readonly Fixture _fixture;

    public EffectTypesTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Fact]
    public void EffectTypeDescription_Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = EffectTypeDescription.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        // Act
        var res = EffectTypeDescription.Create(invalidStringMock);

        // Assert
        var expectedErrors = new List<IError>() { EffectTypeDescriptionErrors.InvalidLength(EffectTypeDescription.MaxLength) }.AsReadOnly();

        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void EffectTypeDescription_Create_Should_ReturnEmptyString(string input)
    {
        // Arrange

        // Act
        var res = EffectTypeDescription.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.ShouldBeEquivalentTo(EffectTypeDescription.Empty);
    }
}
