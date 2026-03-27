using Led.Api.ArchitectureTests.Extensions;
using NetArchTest.Rules;

namespace Led.Api.ArchitectureTests.Layers;

public class LayerTests : BaseTest
{
    #region SharedKernal Layer
    [Fact]
    public void SharedKernalLayer_ShouldNot_HaveDependencyOnDomain()
    {
        var result = Types.InAssembly(SharedKernalAssembly)
            .ShouldNot()
            .HaveDependencyOn(DomainAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void SharedKernalLayer_ShouldNot_HaveDependencyOnApplication()
    {
        var result = Types.InAssembly(SharedKernalAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void SharedKernalLayer_ShouldNot_HaveDependencyOnInfrastructure()
    {
        var result = Types.InAssembly(SharedKernalAssembly)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void SharedKernalLayer_ShouldNot_HaveDependencyOnPresentation()
    {
        var result = Types.InAssembly(SharedKernalAssembly)
            .ShouldNot()
            .HaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }
    #endregion

    #region Domain Layer
    [Fact]
    public void DomainLayer_ShouldNot_HaveDependencyOnApplication()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void DomainLayer_ShouldNot_HaveDependencyOnInfrastructure()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void DomainLayer_ShouldNot_HaveDependencyOnPresentation()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }
    #endregion

    #region Application Layer
    [Fact]
    public void ApplicationLayer_ShouldNot_HaveDependencyOnInfrastructure()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void ApplicationLayer_ShouldNot_HaveDependencyOnPresentation()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }
    #endregion

    #region Infrastructure Layer
    [Fact]
    public void InfrastructureLayer_ShouldNot_HaveDependencyOnPresentation()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsValid();
    }
    #endregion
}
