using BusinessLogicLayer.PollyContracts;
using Polly;
using Polly.Retry;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer.Policies;

public class UserMircoservicePollyPolicies : IUserMicroServicePolicies
{
    private readonly ILogger<UserMircoservicePollyPolicies> _logger;

    public UserMircoservicePollyPolicies(ILogger<UserMircoservicePollyPolicies> logger)
    {
        _logger = logger;
    }
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        AsyncRetryPolicy<HttpResponseMessage> policy =  Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode
        ).WaitAndRetryAsync(retryCount: 3,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            onRetry:
            (exception, timeSpan, retryCount, context) =>
            {
                _logger.LogInformation($"Retrying {retryCount} after   {timeSpan.TotalSeconds} seconds");
            });
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
}