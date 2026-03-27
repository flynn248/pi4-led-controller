using Led.Api.ArchitectureTests.Extensions;
using Led.Domain.Abstraction;
using NetArchTest.Rules;

namespace Led.Api.ArchitectureTests.LayerTests;

public class RepositoryTests : BaseTest
{
    [Fact]
    public void IRepository_ShouldHave_NameEndingWith_Repository()
    {
        var result = Types.InAssemblies([DomainAssembly, InfrastructureAssembly])
            .That()
            .ImplementInterface(typeof(IRepository<,>))
            .And()
            .AreNotAbstract()
            .Should()
            .HaveNameEndingWith("Repository", StringComparison.Ordinal)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void IRepository_ShouldNot_HaveImplementation()
    {
        var result = Types.InAssemblies([DomainAssembly, ApplicationAssembly, PresentationAssembly, SharedKernalAssembly])
            .That()
            .AreNotInterfaces()
            .ShouldNot()
            .ImplementInterface(typeof(IRepository<,>))
            .GetResult();

        result.IsValid();
    }
}
