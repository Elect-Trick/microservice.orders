using Polly;

namespace BusinessLogicLayer.PollyContracts;

public interface IUserMicroServicePolicies : IAsyncPolicy<HttpResponseMessage>
{
 public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();
 public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy();
 public IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy();
 IAsyncPolicy<HttpResponseMessage> GetAllPolcies();
}