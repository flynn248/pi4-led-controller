using System.Text.RegularExpressions;

namespace Led.WebApi.Extensions;

public partial class KebabParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
#pragma warning disable CA1308
        return value is null ? null : SplitCamelCaseRegex().Replace(value.ToString()!, "$1-$2").ToLowerInvariant();
#pragma warning restore
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex SplitCamelCaseRegex();
}
