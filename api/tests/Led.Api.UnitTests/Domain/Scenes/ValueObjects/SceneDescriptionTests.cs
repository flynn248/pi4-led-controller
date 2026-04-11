using AutoFixture;
using FluentResults;
using Led.Domain.Scenes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.Domain.Scenes.ValueObjects;

public class SceneDescriptionTests
{
    private readonly Fixture _fixture;

    public SceneDescriptionTests()
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
        var expected = SceneDescription.Empty;

        // Act
        var res = SceneDescription.Create(input);

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
        var res = SceneDescription.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void Create_Should_ReturnValidationError_WhenInputTooLarge()
    {
        // Arrange
        var invalidLengthMock = SceneDescription.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { SceneDescriptionErrors.InvalidLength(SceneDescription.MaxLength) }.AsReadOnly();

        // Act
        var res = SceneDescription.Create(invalidStringMock);

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
        var instance1 = SceneDescription.Create(input).Value;
        var instance2 = SceneDescription.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
