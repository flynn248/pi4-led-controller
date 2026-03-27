using Led.Api.ArchitectureTests.Extensions;
using Led.SharedKernal.DDD;
using NetArchTest.Rules;

namespace Led.Api.ArchitectureTests.DomainTests;

public class DomainTests : BaseTest
{
    [Fact]
    public void Entities_ShouldHave_PrivateParameterlessConstructor()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity<>))
            .Should()
            .HavePrivateParameterlessConstructor()
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void DomainEvent_ShouldHave_NameEndingWith_DomainEvent()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent", StringComparison.Ordinal)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void DomainEvents_ShouldBe_PublicSealedRecord()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BePublic()
            .And()
            .BeSealed()
            .And()
            .BeClasses()
            .GetResult();

        result.IsValid();
    }
}


