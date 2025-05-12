using Toxiq.Mobile.Dto;
using UnSocial.WebApp.Services;

namespace Unsocial.WebApp.Services.OnlineDataService
{
    public interface IPostService
    {
        Task<SearchResultDto<BasePost>> GetFeed(GetPostDto filter);
        Task<BasePost> GetPost(Guid postId);
        Task<BasePost> GetPrompt(Guid postId);
        Task Publish(BasePost post);
        Task Upvote(Guid id);
        Task Downvote(Guid id);
    }

    public class PostService : IPostService
    {
        private HttpHandler HttpHandler;

        public PostService(HttpHandler httpHandler)
        {
            this.HttpHandler = httpHandler;
        }

        public async Task<SearchResultDto<BasePost>> GetFeed(GetPostDto filter)
        {
            return await HttpHandler.PostJsonAsync<SearchResultDto<BasePost>>("Post/Feed", filter);
        }

        public async Task<BasePost> GetPost(Guid postId)
        {
            return await HttpHandler.GetJsonAsync<BasePost>($"Post/GetPost/{postId}");
        }

        public async Task<BasePost> GetPrompt(Guid postId)
        {
            return await HttpHandler.GetJsonAsync<BasePost>($"Post/GetPrompt/{postId}");
        }

        public async Task Publish(BasePost post)
        {
            await HttpHandler.PostJsonAsync<Comment>("Post/Publish", post);
        }

        public async Task Upvote(Guid id)
        {
            await HttpHandler.GetAsync($"Post/Upvote/{id}");
        }

        public async Task Downvote(Guid id)
        {
            await HttpHandler.GetAsync($"Post/Downvote/{id}");
        }
    }
}
