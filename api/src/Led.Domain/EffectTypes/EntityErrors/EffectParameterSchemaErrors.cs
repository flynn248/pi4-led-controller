using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.EffectTypes.EntityErrors;

public static class EffectParameterSchemaErrors
{
    private const string _baseErrorCode = "effect_parameter_schema";
    public const string BooleanInvalidFormatErrorCode = $"{_baseErrorCode}.data_type.boolean.invalid_foramt";
    public const string WholeNumberInvalidFormatErrorCode = $"{_baseErrorCode}.data_type.whole_number.invalid_foramt";
    public const string RationalNumberInvalidFormatErrorCode = $"{_baseErrorCode}.data_type.rational_number.invalid_foramt";
    public const string CollectionInvalidFormatErrorCode = $"{_baseErrorCode}.data_type.collection.invalid_foramt";

    public static Error BooleanInvalidFormat => new Error("Cannot define minValue, maxValue, or allowedValues for provided data type").Validation(BooleanInvalidFormatErrorCode);
    public static Error WholeNumberInvalidFormat => new Error("Cannot define allowedValues for provided data type").Validation(WholeNumberInvalidFormatErrorCode);
    public static Error WholeNumberMissingRequired => new Error("The minValue and maxValue needs to be defined").Validation(WholeNumberInvalidFormatErrorCode);
    public static Error RationalNumberInvalidFormat => new Error("Cannot define allowedValues for provided data type").Validation(RationalNumberInvalidFormatErrorCode);
    public static Error RationalNumberMissingRequired => new Error("The minValue and maxValue needs to be defined").Validation(RationalNumberInvalidFormatErrorCode);
    public static Error CollectionInvalidFormat => new Error("Cannot define minValue or maxValue for provided data type").Validation(CollectionInvalidFormatErrorCode);
    public static Error CollectionMissingRequired => new Error("The allowedValues need to be defined").Validation(CollectionInvalidFormatErrorCode);
}
