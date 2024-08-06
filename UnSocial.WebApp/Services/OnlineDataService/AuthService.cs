using Toxiq.Mobile.Dto;
using UnSocial.WebApp.Services;

namespace Unsocial.WebApp.Services.OnlineDataService
{
    public interface IAuthService
    {
        bool IsAuth { get; }
        Task Reset();
        Task<bool> CheckHeartBeat();
        Task<LoginResponse> Login(LoginDto login);
    }

    public class AuthService : IAuthService
    {
        public bool IsAuth { get; set; } = false;
        private ITelegramJsInterop TelegramJsInterop;

        private HttpHandler HttpHandler;

        public AuthService(ITelegramJsInterop telegramJsInterop, HttpHandler httpHandler)
        {
            TelegramJsInterop = telegramJsInterop;
            HttpHandler = httpHandler;
            IsAuth = false;

        }

        public async Task<bool> CheckHeartBeat()
        {
            try
            {
                var result = await HttpHandler.GetAsync("Auth/Ping");
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }



        public async Task Reset()
        {

            await HttpHandler.GetAsync("Auth/Reset");
        }

        public async Task<LoginResponse> Login(LoginDto login)
        {
            try
            {

                var result = await HttpHandler.PostJsonAsync<LoginResponse>("Auth/TG_WEB_LOGIN", login);

                HttpHandler.SetToken(result.token);
                IsAuth = true;
                // await TelegramJsInterop.SaveLoginToken(result.token);
                return result;
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    token = "NA",
                };
            }
        }
    }
}
