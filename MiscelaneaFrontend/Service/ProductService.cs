using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Routing;
using MiscelaneaFrontend.Models;

namespace MiscelaneaFrontend.Service
{
    public class ProductService
    {
        private readonly string urlBase = "http://localhost:5160/";
        private HttpClient _httpClient;
        public ProductService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(urlBase);
            _httpClient.DefaultRequestHeaders.AcceptCharset.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<(int, List<ProductDTO>)> GetProductsAsync()
        {
            try
            {
                List<ProductDTO> productList = new List<ProductDTO>();
                HttpResponseMessage request = await _httpClient.GetAsync("Product/obtenerProductos");
                int code = 500;
                if (request.IsSuccessStatusCode)
                {
                    productList = await request.Content.ReadFromJsonAsync<List<ProductDTO>>();
                    //productList = await JsonSerializer.DeserializeAsync<List<ProductDTO>>(responseBody);
                    code = 200;
                }          
                return (code, productList);
            }
            catch (Exception ex)
            {
                return (500, new List<ProductDTO>());
            }
        }

        public async Task<int> PostProductAsync(ProductDTO product)
        {
            var response = await _httpClient.PutAsJsonAsync("Product/registrarProducto", product);

            return response.IsSuccessStatusCode ?  200 :  500;
            
        }
    }
}
