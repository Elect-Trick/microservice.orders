using Polly;

namespace BusinessLogicLayer.PollyContracts;

public interface IUserMicroServicePolicies : IAsyncPolicy<HttpResponseMessage>
{
 public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();
}