using System.Text.Json;

namespace UnSocial.WebApp.Services
{
    public class CustomHttpClientHandler : HttpClientHandler
    {
        private readonly TokenService _tokenService;
        private static string host = "https://api.toxiq.xyz/api/";
        private static JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public CustomHttpClientHandler(TokenService tokenService)
        {
            _tokenService = tokenService;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if (request.RequestUri.AbsolutePath != "/api/Auth/TG_WEB_LOGIN")
            //{
            //    var token = await _tokenService.GetUserTokenAsync();
            //    if (!string.IsNullOrEmpty(token))
            //    {
            //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //    }
            //}

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
