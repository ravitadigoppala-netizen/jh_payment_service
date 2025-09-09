using jh_payment_service.Model;
using System.Text;
using System.Text.Json;

namespace jh_payment_service.Service
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<HttpClientService> _logger;

        public HttpClientService(IHttpClientFactory clientFactory, ILogger<HttpClientService> logger)
        {
            _clientFactory = clientFactory;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        // GET
        public async Task<T?> GetAsync<T>(string url)
        {
            var content = string.Empty;

            try
            {
                var client = _clientFactory.CreateClient("PaymentDb-Microservice");
                var response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();
                content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<MicroserviceResponse<T>>(content, _jsonOptions);
                return result!.ResponseBody;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }

        // POST
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            var client = _clientFactory.CreateClient("PaymentDb-Microservice");
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(result, _jsonOptions);
        }

        // PUT
        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data)
        {
            var client = _clientFactory.CreateClient("PaymentDb-Microservice");
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(result, _jsonOptions);
        }

        // DELETE
        public async Task<bool> DeleteAsync(string url)
        {
            var client = _clientFactory.CreateClient("PaymentDb-Microservice");
            var response = await client.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
    }
}
