using AutoFixture;
using FluentResults;
using Led.Domain.Devices.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.Devices.ValueObjects;

public class DeviceNameTests
{
    private readonly Fixture _fixture;

    public DeviceNameTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_ShouldFail_AndReturnEmpty(string input)
    {
        // Arrange
        var expectedErrors = new List<IError>() { DeviceNameErrors.Empty }.AsReadOnly();

        // Act
        var res = DeviceName.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Fact]
    public void Create_Should_ReturnInvalidLength()
    {
        // Arrange
        var invalidLengthMock = DeviceName.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { DeviceNameErrors.InvalidLength(DeviceName.MaxLength) }.AsReadOnly();

        // Act
        var res = DeviceName.Create(invalidStringMock);

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
        var res = DeviceName.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void TwoObjects_WithSameValue_Should_BeEqual()
    {
        // Arrange
        const string input = "test valid input";

        // Act
        var instance1 = DeviceName.Create(input).Value;
        var instance2 = DeviceName.Create(input).Value;

        // Assert
        instance1.ShouldBeEquivalentTo(instance2);
    }
}
