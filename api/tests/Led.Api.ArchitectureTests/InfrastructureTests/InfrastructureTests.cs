using Led.Api.ArchitectureTests.Extensions;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;

namespace Led.Api.ArchitectureTests.InfrastructureTests;

public class InfrastructureTests : BaseTest
{
    [Fact]
    public void EFCore_EntityTypeConfiguration_ShouldNot_BePublic()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void EFCore_EntityTypeConfiguration_Should_BeSealed()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsValid();
    }
}
