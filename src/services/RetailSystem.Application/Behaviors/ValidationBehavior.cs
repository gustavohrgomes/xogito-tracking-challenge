using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace RetailSystem.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        IEnumerable<Task<ValidationResult>> validationTasks = _validators.Select(validator => validator.ValidateAsync(request, cancellationToken));

        ValidationResult[] validationResults = await Task.WhenAll(validationTasks);

        IEnumerable<ValidationFailure> validationFailures = validationResults.SelectMany(result => result.Errors);

        if (!validationFailures.Any())
        {
            return await next().ConfigureAwait(continueOnCapturedContext: false);
        }

        throw new ValidationException(validationFailures);
    }
}
