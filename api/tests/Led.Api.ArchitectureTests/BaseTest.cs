using System.Reflection;

namespace Led.Api.ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly SharedKernalAssembly = typeof(Led.SharedKernal.DependencyInjection).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(Led.Domain.Tenants.User).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(Led.Application.DependencyInjection).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(Led.Infrastructure.DependencyInjection).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(WebApi.Controllers.Users.UserController).Assembly;
}
