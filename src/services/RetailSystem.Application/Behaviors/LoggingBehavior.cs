using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Tracking.Application.Extensions;

namespace Warehouse.Tracking.Application.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string behaviorName = typeof(LoggingBehavior<TRequest, TResponse>).ResolveTypeName();

        string requestType = typeof(TRequest).Name;

        try
        {
            _logger.LogInformation("[{Behavior}] - Handling request of type {RequestType}", behaviorName, requestType);

            var response = await next().ConfigureAwait(continueOnCapturedContext: false);

            if (response.IsSuccess)
            {
                _logger.LogInformation("[{Behavior}] - Request of type {RequestType} handled successfully", behaviorName, requestType);
                return response;
            }

            _logger.LogInformation("[{Behavior}] - Request of type {RequestType} handled with errors {Errors}", behaviorName, requestType, response.Errors);
            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "[{Behavior}] - An exception occurred while handling request of type {RequestType}", behaviorName, requestType);

            throw;
        }
    }
}
