using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace UnSocial.WebApp.Services
{
    public class HttpHandler
    {
        private HttpClient client;

        private string host = "http://10.0.10.3:9745/api/";
        private JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public async void SetToken(string token)
        {
            //await tokenService.SetUserTokenAsync(token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public void SetHost(string hostUrl) => host = hostUrl;

        private readonly TokenService tokenService;


        public HttpHandler(IHttpClientFactory ClientFactory, TokenService TokenService)
        {
            client = ClientFactory.CreateClient("ApiClient");
            tokenService = TokenService;
        }
        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            try
            {
                return await client.GetAsync($"{host}{path}");
            }
            catch (Exception ex)
            {
                HandleExeption(ex);
                return default(HttpResponseMessage);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string path, T obj)
        {
            try
            {
                return await client.PostAsJsonAsync($"{host}{path}", obj);
            }
            catch (Exception ex)
            {
                HandleExeption(ex);
                return default(HttpResponseMessage);
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string path, T obj)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var content = new StringContent(JsonSerializer.Serialize(obj, options), Encoding.UTF8, "application/json");
                return await client.PutAsync($"{host}{path}", content);
            }
            catch (Exception ex)
            {
                HandleExeption(ex);
                return default(HttpResponseMessage);
            }
        }

        public async Task<T> PostJsonAsync<T>(string path, object obj)
        {
            try
            {
                var response = await client.PostAsJsonAsync($"{host}{path}", obj);
                if (response.IsSuccessStatusCode)
                {
                    var jsonsString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(jsonsString, options);
                }
                else
                {
                    HandlerError(response);
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                HandleExeption(ex);
                return default(T);
            }
        }

        public async Task<T> GetJsonAsync<T>(string path)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var response = await client.GetAsync($"{host}{path}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonsString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(jsonsString, options);
                }
                else
                {
                    HandlerError(response);
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                HandleExeption(ex);
                return default(T);
            }
        }

        public async Task<bool> PutJsonAsync(string path, object obj)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var content = new StringContent(JsonSerializer.Serialize(obj, options), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{host}{path}", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    HandlerError(response);
                    return false;
                }
            }
            catch (Exception ex)
            {
                HandleExeption(ex);
                return default(bool);
            }
        }

        private async void HandlerError(HttpResponseMessage response)
        {
            var msg = await response.Content.ReadAsStringAsync();
        }


        private async void HandleExeption(Exception ex)
        {
            switch (ex.Message)
            {
                case "Connection refused":
                    break;
                default:
                    break;
            }
        }
    }
}
