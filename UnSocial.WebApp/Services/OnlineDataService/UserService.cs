using Toxiq.Mobile.Dto;
using UnSocial.WebApp.Services;

namespace Unsocial.WebApp.Services.OnlineDataService
{
    public interface IUserService
    {
        Task<bool> ChangeUsername(string username);
        Task<bool> CheckUsername(string username);
        Task<UserProfile> GetMe();
        Task EditProfile(UserProfile profile);
        Task<UserProfile> GetUser(string username);

        Task<List<BasePost>> GetUserPosts(string username);
        Task<List<BasePost>> GetUserWallPosts(string username);
    }

    public class UserService : IUserService
    {
        private HttpHandler HttpHandler;

        public UserService(HttpHandler httpHandler)
        {
            HttpHandler = httpHandler;
        }

        public async Task<bool> ChangeUsername(string username)
        {
            var result = await HttpHandler.GetAsync($"User/ChangeUsername?username={username}");
            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> CheckUsername(string username)
        {
            var result = await HttpHandler.GetAsync($"User/CheckUsername?username={username}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<UserProfile> GetMe()
        {
            return await HttpHandler.GetJsonAsync<UserProfile>("User/GetMe");
        }

        public async Task EditProfile(UserProfile profile)
        {
            await HttpHandler.PostAsync("User/EditUserProfile", profile);
        }

        public async Task<UserProfile> GetUser(string username)
        {
            return await HttpHandler.GetJsonAsync<UserProfile>($"User/GetUser/{username}");
        }


        public async Task<List<BasePost>> GetUserPosts(string username)
        {
            return await HttpHandler.GetJsonAsync<List<BasePost>>($"User/GetUserPosts/{username}");
        }

        public async Task<List<BasePost>> GetUserWallPosts(string username)
        {
            return await HttpHandler.GetJsonAsync<List<BasePost>>($"User/GetUserWallPosts/{username}");
        }
    }
}
