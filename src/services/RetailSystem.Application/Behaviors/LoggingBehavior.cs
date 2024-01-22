using MediatR;
using Microsoft.Extensions.Logging;

namespace RetailSystem.Application.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string behaviorName = nameof(LoggingBehavior<TRequest, TResponse>);

        string requestType = typeof(TRequest).Name;

        try
        {
            _logger.LogInformation("[{Behavior}] - Handling request of type {RequestType}", behaviorName, requestType);

            var response = await next().ConfigureAwait(continueOnCapturedContext: false);

            _logger.LogInformation("[{Behavior}] - Request of type {RequestType} handled successfully", behaviorName, requestType);

            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "[{Behavior}] - An exception occurred while handling request of type {RequestType}", behaviorName, requestType);

            throw;
        }
    }
}
