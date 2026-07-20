using System.Net;
using System.Text;
using System.Text.Json;
using BusinessLogicLayer.PollyContracts;
using DataAccessLayer.DTOs;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Bulkhead;
using Polly.Fallback;

namespace BusinessLogicLayer.Policies;

public class ProdcutsMicroservicePollyPolicies: IProductsMicroServicePolices
{
    private readonly ILogger<ProdcutsMicroservicePollyPolicies> _logger;
    private IAsyncPolicy<HttpResponseMessage> _combinedPolicies;

    public ProdcutsMicroservicePollyPolicies(ILogger<ProdcutsMicroservicePollyPolicies> logger)
    {
        _logger = logger;
        _combinedPolicies = Policy.WrapAsync(
            GetFallBackPolicy(),
            GetBulkheadIsolationPolicy()
        );
    }

    public IAsyncPolicy<HttpResponseMessage> GetFallBackPolicy()
    {
        AsyncFallbackPolicy<HttpResponseMessage> policy =  Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
            .FallbackAsync(async (context) =>
            {
                _logger.LogInformation($"Falling back has been triggered, returning dummy values");
                ProductDTO product = new ProductDTO
                {
                    Category = "Currently unavailable",
                    ProductName = "Currently unavailable",
                    UnitPrice = 1,
                    QuantityInStock = 1
                };
                return new HttpResponseMessage(
                    statusCode: HttpStatusCode.OK){
                    Content = new StringContent(JsonSerializer.Serialize(product),Encoding.UTF8, "application/json")};
                
            });
        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy()
    {
        AsyncBulkheadPolicy<HttpResponseMessage> policy = Policy.BulkheadAsync<HttpResponseMessage>(2, maxQueuingActions:40, onBulkheadRejectedAsync:
            (context) =>
            {
                _logger.LogInformation($"Bulkhead has been triggered, Cant serve more requests");
                return Task.CompletedTask;
            });
        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetAllPolicies()
    {
        return _combinedPolicies;
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