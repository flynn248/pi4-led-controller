using FluentResults;
using Led.Domain.EffectTypes.EntityErrors;
using Led.Domain.EffectTypes.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.EffectTypes;

public sealed class EffectParameterSchema : Entity<Guid>
{
    public Guid EffectTypeId { get; init; }
    public Guid? ParentId { get; private set; }
    public ParameterKey Key { get; private set; }
    public ParameterDataTypeId DataTypeId { get; private set; }
    public bool IsRequired { get; private set; }
    public double? MinValue { get; private set; }
    public double? MaxValue { get; private set; }
    public ParameterAllowedValues AllowedValues { get; private set; }
    public ParameterDescription Description { get; private set; }

    private EffectParameterSchema()
    { }

    private EffectParameterSchema(Guid id, Guid effectTypeId, Guid? parentId, ParameterKey key, ParameterDataTypeId dataTypeId, bool isRequired, double? minValue, double? maxValue, ParameterAllowedValues allowedValues, ParameterDescription description)
        : base(id)
    {
        EffectTypeId = effectTypeId;
        ParentId = parentId;
        Key = key;
        DataTypeId = dataTypeId;
        IsRequired = isRequired;
        MinValue = minValue;
        MaxValue = maxValue;
        AllowedValues = allowedValues;
        Description = description;
    }

    public static Result<EffectParameterSchema> Create(Guid id, Guid effectTypeId, ParameterKey key, ParameterDataTypeId dataTypeId, bool isRequired, double? minValue, double? maxValue, ParameterAllowedValues allowedValues, ParameterDescription description)
    {
        var validation = ValidateInput(dataTypeId, minValue, maxValue, allowedValues);

        if (validation.IsFailed)
        {
            return Result.Fail(validation.Errors);
        }

        return new EffectParameterSchema(Guid.CreateVersion7(), effectTypeId, null, key, dataTypeId, isRequired, minValue, maxValue, allowedValues, description);
    }

    public static Result<EffectParameterSchema> Create(Guid parentId, Guid id, Guid effectTypeId, ParameterKey key, ParameterDataTypeId dataTypeId, bool isRequired, double? minValue, double? maxValue, ParameterAllowedValues allowedValues, ParameterDescription description)
    {
        var validation = ValidateInput(dataTypeId, minValue, maxValue, allowedValues);

        if (validation.IsFailed)
        {
            return Result.Fail(validation.Errors);
        }

        return new EffectParameterSchema(Guid.CreateVersion7(), effectTypeId, parentId, key, dataTypeId, isRequired, minValue, maxValue, allowedValues, description);
    }

    private static Result ValidateInput(ParameterDataTypeId dataTypeId, double? minValue, double? maxValue, ParameterAllowedValues allowedValues)
    {
        switch (dataTypeId)
        {
            case ParameterDataTypeId.Boolean:
                if (minValue is not null
                    || maxValue is not null
                    || allowedValues != ParameterAllowedValues.Empty)
                {
                    return Result.Fail(EffectParameterSchemaErrors.BooleanInvalidFormat);
                }

                return Result.Ok();
            case ParameterDataTypeId.WholeNumber:
                if (allowedValues != ParameterAllowedValues.Empty)
                {
                    return Result.Fail(EffectParameterSchemaErrors.WholeNumberInvalidFormat);
                }
                else if (minValue is null || maxValue is null)
                {
                    return Result.Fail(EffectParameterSchemaErrors.WholeNumberMissingRequired);
                }

                return Result.Ok();
            case ParameterDataTypeId.RationalNumber:
                if (allowedValues != ParameterAllowedValues.Empty)
                {
                    return Result.Fail(EffectParameterSchemaErrors.RationalNumberInvalidFormat);
                }
                else if (minValue is null || maxValue is null)
                {
                    return Result.Fail(EffectParameterSchemaErrors.RationalNumberMissingRequired);
                }

                return Result.Ok();
            case ParameterDataTypeId.Collection:
                if (minValue is not null || maxValue is not null)
                {
                    return Result.Fail(EffectParameterSchemaErrors.CollectionInvalidFormat);
                }
                else if (allowedValues == ParameterAllowedValues.Empty)
                {
                    return Result.Fail(EffectParameterSchemaErrors.CollectionMissingRequired);
                }

                return Result.Ok();
            case ParameterDataTypeId.Complex:
                if (minValue is not null
                    || maxValue is not null
                    || allowedValues != ParameterAllowedValues.Empty)
                {
                    return Result.Fail(EffectParameterSchemaErrors.ComplexInvalidFormat);
                }

                return Result.Ok();
            default:
                throw new NotImplementedException($"Provided DataTypeId of {dataTypeId} is not supported");
        }
    }
}
