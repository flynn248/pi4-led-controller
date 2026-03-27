using Led.Api.ArchitectureTests.Extensions;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;
using NetArchTest.Rules;

namespace Led.Api.ArchitectureTests.ApplicationTests;

public class ApplicationTests : BaseTest
{
    [Fact]
    public void Command_ShouldHave_NameEndingWith_Command()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommand))
            .Or()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .HaveNameEndingWith("Command", StringComparison.Ordinal)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void Command_ShouldBe_PublicSealedRecord()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommand))
            .Or()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .BePublic()
            .And()
            .BeSealed()
            .And()
            .BeClasses()
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void CommandHandler_ShouldHave_NameEndingWith_CommandHandler()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler", StringComparison.Ordinal)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void CommandHandler_ShouldNot_BePublic()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void CommandHandler_Should_BeSealed()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void Query_ShouldHave_NameEndingWith_Query()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQuery))
            .Or()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .HaveNameEndingWith("Query", StringComparison.Ordinal)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void QueryHandler_ShouldHave_NameEndingWith_QueryHandler()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler", StringComparison.Ordinal)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void QueryHandler_ShouldNot_BePublic()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void QueryHandler_Should_BeSealed()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void Validator_ShouldHave_NameEndingWith_Validator()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryValidator<>))
            .Or()
            .ImplementInterface(typeof(ICommandValidator<>))
            .Should()
            .HaveNameEndingWith("Validator", StringComparison.Ordinal)
            .GetResult();

        result.IsValid();
    }

    [Fact]
    public void Validator_Should_BeSealed()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryValidator<>))
            .Or()
            .ImplementInterface(typeof(ICommandValidator<>))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsValid();
    }
}


