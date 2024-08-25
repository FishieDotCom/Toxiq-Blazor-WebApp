using Microsoft.JSInterop;

namespace UnSocial.WebApp.Services
{
    public interface ITelegramJsInterop
    {
        Task<string> InitializeTelegramWebApp();
        Task ShowButton();
        Task HideButton();
        Task SaveToCloud(string key, string value);
        Task<string> LoadFromCloud(string key);
        Task Close();
        Task ShowAlert(string key);
        Task ShareToTele(string url);
        Task<string> GetLoginToken();
        Task SaveLoginToken(string token);
    }

    public class TelegramJsInterop : ITelegramJsInterop
    {
        private readonly IJSRuntime _jsRuntime;

        public TelegramJsInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> InitializeTelegramWebApp()
        {
            return await _jsRuntime.InvokeAsync<string>("initializeTelegramWebApp");
        }

        public async Task ShowButton()
        {
            await _jsRuntime.InvokeVoidAsync("showButton");
        }

        public async Task HideButton()
        {
            await _jsRuntime.InvokeVoidAsync("hideButton");
        }

        public async Task SaveToCloud(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("saveToCloud", key, value);
        }

        public async Task Close()
        {
            await _jsRuntime.InvokeVoidAsync("close");

        }
        public async Task<string> LoadFromCloud(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("loadFromCloud", key);
        }

        public async Task ShareToTele(string url)
        {
            await _jsRuntime.InvokeVoidAsync("shareToTele", url);
        }


        public async Task ShowAlert(string key)
        {
            await _jsRuntime.InvokeVoidAsync("showAlert", key);
        }

        public async Task SaveLoginToken(string token)
        {
            await _jsRuntime.InvokeVoidAsync("LSset", "token", token);
        }

        public async Task<string> GetLoginToken()
        {
            return await _jsRuntime.InvokeAsync<string>("LSget", "token");

        }
    }
}
