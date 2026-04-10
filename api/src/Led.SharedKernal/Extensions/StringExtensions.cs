using System.Text.RegularExpressions;

namespace Led.SharedKernal.Extensions;

public static partial class StringExtensions
{
    public static bool IsEmptyJson(this string value)
    {
        return IsEmptyJson().IsMatch(value);
    }

    [GeneratedRegex(@"^[\s{}\[\]]+$")]
    private static partial Regex IsEmptyJson();
}
