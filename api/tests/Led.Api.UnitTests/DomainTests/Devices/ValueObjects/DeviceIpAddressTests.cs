using AutoFixture;
using FluentResults;
using Led.Domain.Devices.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.Devices.ValueObjects;

public class DeviceIpAddressTests
{
    private readonly Fixture _fixture;

    public DeviceIpAddressTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(" a ")]
    [InlineData("a")]
    [InlineData("172.0.0")]
    [InlineData("172.0.0.999")]
    [InlineData("172.0.0.255.")]
    [InlineData("172.0.0.255.88")]
    public void Create_Should_ReturnInvalid(string input)
    {
        // Arrange
        var expectedError = new List<IError>() { DeviceIpAddressErrors.Invalid }.AsReadOnly();

        // Act
        var res = DeviceIpAddress.Create(input);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedError);
    }

    [Fact]
    public void Create_Should_ReturnInvalidLength()
    {
        // Arrange
        var invalidLengthMock = DeviceIpAddress.MaxLength + _fixture.Create<int>();
        var invalidStringMock = string.Join("", _fixture.CreateMany<char>(invalidLengthMock));

        var expectedErrors = new List<IError>() { DeviceIpAddressErrors.InvalidLength(DeviceIpAddress.MaxLength) };

        // Act
        var res = DeviceIpAddress.Create(invalidStringMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [InlineData("127.0.0.0")]
    [InlineData("127.0.0.255")]
    [InlineData("127.0.255.255")]
    [InlineData("127.255.255.255")]
    [InlineData(" 127.255.255.255 ")]
    public void Create_Should_ReturnSuccess(string input)
    {
        // Arrange
        var expectedValue = input.Trim();

        // Act
        var res = DeviceIpAddress.Create(input);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.Value.ShouldBeEquivalentTo(expectedValue);
    }
}
