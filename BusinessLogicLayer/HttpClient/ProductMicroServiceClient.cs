using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DataAccessLayer.DTOs;
using Microsoft.Extensions.Caching.Distributed;

namespace BusinessLogicLayer.HttpClient;
using System.Net.Http;
public class ProductMicroServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _distributedCache;
    public ProductMicroServiceClient(HttpClient httpClient, IDistributedCache distributedCache)
    {
        _httpClient = httpClient;
        _distributedCache = distributedCache;
    }
    
    public async Task<ProductDTO?> GetProductById(int id)
    {
        string cacheKey = $"product:{id}";  
        string? cachedProduct =  await _distributedCache.GetStringAsync(cacheKey);
        if (cachedProduct != null)
        {
            return JsonSerializer.Deserialize<ProductDTO>(cachedProduct);
        }
        
        HttpResponseMessage response = await _httpClient.GetAsync($"api/products/search/productId/{id}");
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException($"Bad Request, Product ID {id} not found ", null,HttpStatusCode.BadRequest);
            }
            throw new HttpRequestException("Internal Server Error", null, response.StatusCode);
        }

        ProductDTO? product = await response.Content.ReadFromJsonAsync<ProductDTO>();
        if (product == null)
        {
            throw new ArgumentNullException("Invalid Product ID");
            
        }
        
        string cacheValue = JsonSerializer.Serialize(product);
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30)).SetSlidingExpiration(TimeSpan.FromSeconds(10));    
        await _distributedCache.SetStringAsync(cacheKey, cacheValue, options);
        return product;

    }
}