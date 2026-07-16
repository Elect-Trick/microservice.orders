using System.Net;
using System.Net.Http.Json;
using DataAccessLayer.DTOs;

namespace BusinessLogicLayer.HttpClient;
using System.Net.Http;
public class ProductMicroServiceClient
{
    private readonly HttpClient _httpClient;
    public ProductMicroServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ProductDTO?> GetProductById(int id)
    {
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
            
        return product;

    }
}