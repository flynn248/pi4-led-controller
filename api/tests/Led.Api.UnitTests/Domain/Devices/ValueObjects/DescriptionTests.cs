using AutoFixture;
using FluentResults;
using Led.Domain.Devices.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.Domain.Devices.ValueObjects;

public class DescriptionTests
{
    private readonly Fixture _fixture;

    public DescriptionTests()
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
        var expected = Description.Empty;

        // Act
        var res = Description.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void Create_Should_TrimWhitespaceOnEnds()
    {
        // Arrange
        const string input = " valid ";

        var expected = input.Trim();

        // Act
        var res = Description.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = Description.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { DescriptionErrors.InvalidLength(Description.MaxLength) }.AsReadOnly();

        // Act
        var res = Description.Create(invalidStringMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("test valid input")]
    public void TwoObjects_WithSameValue_Should_BeEqual(string input)
    {
        // Arrange

        // Act
        var instance1 = Description.Create(input).Value;
        var instance2 = Description.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
