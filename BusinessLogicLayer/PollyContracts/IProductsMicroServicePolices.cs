using Polly;

namespace BusinessLogicLayer.PollyContracts;

public interface IProductsMicroServicePolices: IAsyncPolicy<HttpResponseMessage>
{
    
    public IAsyncPolicy<HttpResponseMessage> GetFallBackPolicy();
    public IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy();
    IAsyncPolicy<HttpResponseMessage> GetAllPolicies();

}