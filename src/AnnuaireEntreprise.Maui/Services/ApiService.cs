
using System.Text;
using System.Text.Json;


namespace AnnuaireEntreprise.Maui.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public ApiService()
        {
            // Create HttpClient
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true // Development only
            };

            _httpClient = new HttpClient(httpClientHandler);

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                _httpClient.BaseAddress = new Uri("https://10.0.2.2:7004/api/"); // For Android Emulator
            }
            else
            {
                _httpClient.BaseAddress = new Uri("https://localhost:7004/api/"); // For Windows Emulator
            }
        }


        private async Task<HttpResponseMessage> SendRequestAsync<T>(HttpMethod method, string endpoint, T? data)
        {
            // Create Request
            var request = new HttpRequestMessage(method, endpoint);

            // Add JSON Content
            if (data != null)
            {
                var json = JsonSerializer.Serialize(data, _jsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            // Send Request
            try
            {
                Console.WriteLine($"Sending {method} request to {endpoint}");
                return await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request to {endpoint} failed: {ex.Message}");
                throw;
            }
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await SendRequestAsync<object>(HttpMethod.Get, endpoint, null);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseString, _jsonOptions);
            }

            Console.WriteLine($"GET {endpoint} failed: {response.StatusCode}");
            return default;
        }

        // Post request. TRequest is the request data type, TResponse is the response data type
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await SendRequestAsync(HttpMethod.Post, endpoint, data);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(responseString, _jsonOptions);
            }

            Console.WriteLine($"POST {endpoint} failed: {response.StatusCode}");
            return default;
        }

        // Post request. T is the request data type
        public async Task<bool> PutAsync<T>(string endpoint, T data)
        {
            var response = await SendRequestAsync(HttpMethod.Put, endpoint, data);
            return response.IsSuccessStatusCode;
        }

        // Delete request
        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await SendRequestAsync<object>(HttpMethod.Delete, endpoint, null);
            return response.IsSuccessStatusCode;
        }
    }
}
