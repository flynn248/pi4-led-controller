using FluentValidation;
using Led.Application.Exceptions;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Abstraction.Behaviors;

internal sealed class ValidationBehavior<TRequest> : ICommandPreHandler<TRequest>//, IQueryPreHandler<TRequest>
    where TRequest : ICommand //, IQuery
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task PreHandleAsync(TRequest message, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        if (!_validators.Any())
        {
            return;
        }

        var context = new ValidationContext<TRequest>(message);

        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(result => result.Errors.Any())
            .SelectMany(result => result.Errors)
            .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage))
            .ToList();

        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }
    }
}
