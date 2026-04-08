using AutoFixture;
using FluentResults;
using Led.Domain.Devices.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.Devices.ValueObjects;

public class SerialNumberTests
{
    private readonly Fixture _fixture;

    public SerialNumberTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_ShouldFail_AndReturnInvalid(string input)
    {
        // Arrange
        var expectedErrors = new List<IError>() { SerialNumberErrors.Invalid }.AsReadOnly();

        // Act
        var res = SerialNumber.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Fact]
    public void Create_Should_ReturnInvalidLength()
    {
        // Arrange
        var invalidLengthMock = SerialNumber.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { SerialNumberErrors.InvalidLength(SerialNumber.MaxLength) }.AsReadOnly();

        // Act
        var res = SerialNumber.Create(invalidStringMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Fact]
    public void Create_Should_TrimEnds()
    {
        // Arrange
        const string input = " valid ";
        var expected = input.Trim();

        // Act
        var res = SerialNumber.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expected);
    }
}
