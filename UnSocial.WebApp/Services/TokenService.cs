using Microsoft.JSInterop;

namespace UnSocial.WebApp.Services
{
    public class TokenService
    {
        private Lazy<IJSObjectReference> _accessorJsRef = new();
        private readonly IJSRuntime _jsRuntime;

        public TokenService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }


        public async Task<string> GetUserTokenAsync()
        {

            var tok = await _jsRuntime.InvokeAsync<string>("token");

            return tok;
        }

        public async Task SetUserTokenAsync(string token)
        {

            await _jsRuntime.InvokeVoidAsync("set", "token", token);
        }
    }
}
