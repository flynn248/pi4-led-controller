using FluentResults;
using Led.Domain.EffectTypes;
using Led.Domain.EffectTypes.EntityErrors;
using Led.Domain.EffectTypes.ValueObjects;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.EffectTypes;

public class EffectParameterSchemaTests
{
    [Theory]
    [MemberData(nameof(Create_ShouldFail_WhenInputIsInvalid_TestData))]
    public void Create_ShouldFail_WhenParameterInputIsInvalid(ParameterDataTypeId dataTypeId, double? minValue, double? maxValue, ParameterAllowedValues parameterAllowedValues, Error expectedError)
    {
        // Arrange
        var keyMock = ParameterKey.Create("key").Value;
        var descMock = ParameterDescription.Create("desc").Value;
        var isRequiredMock = false;

        var expectedErrors = new List<IError>() { expectedError }.AsReadOnly();

        // Act
        var res = EffectParameterSchema.Create(Guid.CreateVersion7(), keyMock, dataTypeId, isRequiredMock, minValue, maxValue, parameterAllowedValues, descMock);

        // Assert
        res.IsFailed.ShouldBeTrue();
        res.Errors.ShouldBeEquivalentTo(expectedErrors);
    }

    [Theory]
    [MemberData(nameof(Create_ShouldPass_WhenParameterInputIsValid_TestData))]
    public void Create_ShouldFail_WhenInputIsValid(ParameterDataTypeId dataTypeId, double? minValue, double? maxValue, ParameterAllowedValues parameterAllowedValues)
    {
        // Arrange
        var keyMock = ParameterKey.Create("key").Value;
        var descMock = ParameterDescription.Create("desc").Value;
        var isRequiredMock = false;

        // Act
        var res = EffectParameterSchema.Create(Guid.CreateVersion7(), keyMock, dataTypeId, isRequiredMock, minValue, maxValue, parameterAllowedValues, descMock);

        // Assert
        res.IsSuccess.ShouldBeTrue();
    }

    public static IEnumerable<TheoryDataRow<ParameterDataTypeId, double?, double?, ParameterAllowedValues, Error>> Create_ShouldFail_WhenInputIsInvalid_TestData()
    {
        var validJson = ParameterAllowedValues.Create("[\"testValidJson\"]").Value;

        yield return new(ParameterDataTypeId.Boolean, 1, null, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.BooleanInvalidFormat);
        yield return new(ParameterDataTypeId.Boolean, null, 1, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.BooleanInvalidFormat);
        yield return new(ParameterDataTypeId.Boolean, null, null, validJson, EffectParameterSchemaErrors.BooleanInvalidFormat);

        yield return new(ParameterDataTypeId.WholeNumber, 1, 1, validJson, EffectParameterSchemaErrors.WholeNumberInvalidFormat);
        yield return new(ParameterDataTypeId.WholeNumber, 1, null, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.WholeNumberMissingRequired);
        yield return new(ParameterDataTypeId.WholeNumber, null, 1, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.WholeNumberMissingRequired);

        yield return new(ParameterDataTypeId.RationalNumber, 1, 1, validJson, EffectParameterSchemaErrors.RationalNumberInvalidFormat);
        yield return new(ParameterDataTypeId.RationalNumber, 1, null, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.RationalNumberMissingRequired);
        yield return new(ParameterDataTypeId.RationalNumber, null, 1, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.RationalNumberMissingRequired);

        yield return new(ParameterDataTypeId.Collection, 1, null, validJson, EffectParameterSchemaErrors.CollectionInvalidFormat);
        yield return new(ParameterDataTypeId.Collection, null, 1, validJson, EffectParameterSchemaErrors.CollectionInvalidFormat);
        yield return new(ParameterDataTypeId.Collection, null, null, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.CollectionMissingRequired);

        yield return new(ParameterDataTypeId.Complex, 1, null, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.ComplexInvalidFormat);
        yield return new(ParameterDataTypeId.Complex, null, 1, ParameterAllowedValues.Empty, EffectParameterSchemaErrors.ComplexInvalidFormat);
        yield return new(ParameterDataTypeId.Complex, null, null, validJson, EffectParameterSchemaErrors.ComplexInvalidFormat);
    }

    public static IEnumerable<TheoryDataRow<ParameterDataTypeId, double?, double?, ParameterAllowedValues>> Create_ShouldPass_WhenParameterInputIsValid_TestData()
    {
        var validJson = ParameterAllowedValues.Create("[\"testValidJson\"]").Value;

        yield return new(ParameterDataTypeId.Boolean, null, null, ParameterAllowedValues.Empty);

        yield return new(ParameterDataTypeId.WholeNumber, 1, 1, ParameterAllowedValues.Empty);
        
        yield return new(ParameterDataTypeId.RationalNumber, 1, 1, ParameterAllowedValues.Empty);
        
        yield return new(ParameterDataTypeId.Collection, null, null, validJson);

        yield return new(ParameterDataTypeId.Complex, null, null, ParameterAllowedValues.Empty);
    }
}
