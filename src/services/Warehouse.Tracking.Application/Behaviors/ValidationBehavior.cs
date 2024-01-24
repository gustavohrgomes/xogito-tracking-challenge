using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Warehouse.Tracking.Application.Exeptions;

namespace Warehouse.Tracking.Application.Behaviors;

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
            var response = await next().ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }

        var errors = validationFailures.Select(failure => new ValidationFailedException.ValidationError(failure.PropertyName, failure.ErrorMessage));

        throw new ValidationFailedException(errors);
    }
}
