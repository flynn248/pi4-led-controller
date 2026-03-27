using Led.Api.ArchitectureTests.Extensions.CustomRules;
using NetArchTest.Rules;
using Shouldly;

namespace Led.Api.ArchitectureTests.Extensions;

internal static class NetArchTestExtension
{
    internal static void IsValid(this NetArchTest.Rules.TestResult result)
    {
        result.ShouldSatisfyAllConditions(
            r => r.IsSuccessful.ShouldBeTrue(),
            r => r.FailingTypeNames.ShouldBeNull());
    }

    internal static ConditionList HavePrivateParameterlessConstructor(this Conditions conditions)
    {
        return conditions.MeetCustomRule(new PrivateConstructorRule());
    }
}
