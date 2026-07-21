using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DataAccessLayer.DTOs;
using Microsoft.Extensions.Caching.Distributed;

namespace BusinessLogicLayer.HttpClient;
using System.Net.Http;
public class UserMicroServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _distributedCache;
    public UserMicroServiceClient(HttpClient httpClient,IDistributedCache distributedCache)
    {
        _httpClient = httpClient;
        _distributedCache = distributedCache;
    }

    public async Task<UserDTO?> GetUserByUserID(Guid id)
    {
        //Depending on where the polly policy takes us, we can return some fall back data like 
        /*
         * return new UserDTO() { Id = Not aavailable at the moment, Name = "Not available at the moment, please try again after 5 min" };
         * Or just return detailed info to the user.
         */
        
        string cacheKey = $"user:{id}";  
        string? cachedProduct =  await _distributedCache.GetStringAsync(cacheKey);
        if (cachedProduct != null)
        {
            return JsonSerializer.Deserialize<UserDTO>(cachedProduct);
        }
        
        HttpResponseMessage response = await _httpClient.GetAsync($"api/user/{id}");
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException("Bad Request, Id not found", null,HttpStatusCode.BadRequest);
            }
            
            
            throw new HttpRequestException("Internal Server Error", null, response.StatusCode);
        }

        UserDTO? user = await response.Content.ReadFromJsonAsync<UserDTO>();
        if (user == null)
        {
            throw new ArgumentNullException("Invalid user ID");
            
        }
        
        string cacheValue = JsonSerializer.Serialize(user);
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30)).SetSlidingExpiration(TimeSpan.FromSeconds(10));    
        await _distributedCache.SetStringAsync(cacheKey, cacheValue, options);
            
        return user;

    }
    
}