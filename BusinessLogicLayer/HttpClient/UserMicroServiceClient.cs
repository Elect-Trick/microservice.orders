using System.Net;
using System.Net.Http.Json;
using DataAccessLayer.DTOs;

namespace BusinessLogicLayer.HttpClient;
using System.Net.Http;
public class UserMicroServiceClient
{
    private readonly HttpClient _httpClient;

    public UserMicroServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDTO?> GetUserByUserID(Guid id)
    {
        //Depending on where the polly policy takes us, we can return some fall back data like 
        /*
         * return new UserDTO() { Id = Not aavailable at the moment, Name = "Not available at the moment, please try again after 5 min" };
         * Or just return detailed info to the user.
         */
        
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
            
        return user;

    }
    
}