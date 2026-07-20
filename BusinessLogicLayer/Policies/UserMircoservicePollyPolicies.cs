using System.Net;
using BusinessLogicLayer.PollyContracts;
using Polly;
using Polly.Retry;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using Polly.Timeout;

namespace BusinessLogicLayer.Policies;

public class UserMircoservicePollyPolicies : IUserMicroServicePolicies
{
    private readonly ILogger<UserMircoservicePollyPolicies> _logger;
    private readonly IAsyncPolicy<HttpResponseMessage> _combinedPolicy;

    public UserMircoservicePollyPolicies(ILogger<UserMircoservicePollyPolicies> logger)
    {
        _logger = logger;
           _combinedPolicy = Policy.WrapAsync(
            GetCircuitBreakerPolicy(),
            GetRetryPolicy(),
            GetTimeoutPolicy()
        );
    }
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        AsyncRetryPolicy<HttpResponseMessage> policy =  Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode
        ).WaitAndRetryAsync(retryCount: 2,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(2),
            onRetry:
            (exception, timeSpan, retryCount, context) =>
            {
                _logger.LogInformation($"Retrying {retryCount} after   {timeSpan.TotalSeconds} seconds");
            });
        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
         AsyncCircuitBreakerPolicy<HttpResponseMessage> policy = Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode
        ).CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 1,
                                durationOfBreak:  TimeSpan.FromSeconds(3),
                                onBreak:
            (outcome, timeSpan) =>
            {
                _logger.LogInformation($"Circuit Breaker tripped for   {timeSpan.TotalMinutes} minutes due to consecutive failures. Subsequest requests will be rejected");
            },
            
            onReset: () =>
            {
                _logger.LogInformation("Circuit Breaker reset, allowing requests to continue");
                                    
            },
                                onHalfOpen: () =>
                                {
                                    _logger.LogInformation("Circuit Breaker half open, testing the service again");
                                });
         
         return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
    {
        AsyncTimeoutPolicy<HttpResponseMessage> policy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(5));
        return policy;
    }


    public string PolicyKey { get; }
    public IAsyncPolicy<HttpResponseMessage> WithPolicyKey(string policyKey)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<Task<HttpResponseMessage>> action)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<Context, Task<HttpResponseMessage>> action, Context context)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<Context, Task<HttpResponseMessage>> action, IDictionary<string, object> contextData)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<CancellationToken, Task<HttpResponseMessage>> action, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, IDictionary<string, object> contextData, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, Context context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<CancellationToken, Task<HttpResponseMessage>> action, CancellationToken cancellationToken, bool continueOnCapturedContext)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, IDictionary<string, object> contextData, CancellationToken cancellationToken,
        bool continueOnCapturedContext)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ExecuteAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, Context context, CancellationToken cancellationToken, bool continueOnCapturedContext)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<Task<HttpResponseMessage>> action)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<Context, Task<HttpResponseMessage>> action, IDictionary<string, object> contextData)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<Context, Task<HttpResponseMessage>> action, Context context)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<CancellationToken, Task<HttpResponseMessage>> action, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, IDictionary<string, object> contextData, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, Context context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<CancellationToken, Task<HttpResponseMessage>> action, CancellationToken cancellationToken, bool continueOnCapturedContext)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, IDictionary<string, object> contextData, CancellationToken cancellationToken,
        bool continueOnCapturedContext)
    {
        throw new NotImplementedException();
    }

    public Task<PolicyResult<HttpResponseMessage>> ExecuteAndCaptureAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, Context context, CancellationToken cancellationToken,
        bool continueOnCapturedContext)
    {
        throw new NotImplementedException();
    }
    
    
    public IAsyncPolicy<HttpResponseMessage> GetAllPolcies()
    {
        // Circuit breaker OUTSIDE, retry INSIDE
        return _combinedPolicy;


    }
}