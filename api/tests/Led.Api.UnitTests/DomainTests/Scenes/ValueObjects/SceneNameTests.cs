using AutoFixture;
using FluentResults;
using Led.Domain.Scenes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.Scenes.ValueObjects;

public class SceneNameTests
{
    private readonly Fixture _fixture;

    public SceneNameTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Should_ReturnEmptyError(string input)
    {
        // Arrange
        var expectedErrors = new List<IError>() { SceneNameErrors.Empty }.AsReadOnly();

        // Act
        var res = SceneName.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Fact]
    public void Create_Should_ReturnInvalidLengthError()
    {
        // Arrange
        var invalidLengthMock = _fixture.Create<int>() + SceneName.MaxLength;
        var invalidInputMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { SceneNameErrors.InvalidLength(SceneName.MaxLength) }.AsReadOnly();

        // Act
        var res = SceneName.Create(invalidInputMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData("valid")]
    [InlineData(" valid ")]
    public void Create_Should_BeSuccessful(string input)
    {
        // Arrange
        var expectedResult = input.Trim();

        // Act
        var res = SceneName.Create(input);

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
        var instance1 = SceneName.Create(input).Value;
        var instance2 = SceneName.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
