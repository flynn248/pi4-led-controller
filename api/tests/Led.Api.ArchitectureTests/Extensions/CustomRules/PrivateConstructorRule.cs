using Mono.Cecil;
using Mono.Cecil.Rocks;
using NetArchTest.Rules;

namespace Led.Api.ArchitectureTests.Extensions.CustomRules;

public class PrivateConstructorRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
    {
        var constructors = type.GetConstructors();

        return constructors.Any(c => c.IsPrivate && !c.HasParameters);
    }
}
