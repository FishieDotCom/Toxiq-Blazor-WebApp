using Toxiq.Mobile.Dto;
using UnSocial.WebApp.Services;

namespace Unsocial.WebApp.Services.OnlineDataService
{
    public interface ICommentService
    {
        Task<SearchResultDto<Comment>> GetPostComments(GetCommentDto filter);
        Task<Comment> CommentOnPost(Comment comment);

        Task<SearchResultDto<Comment>> GetCommentReplies(GetCommentDto filter);
        Task ReplyToComment(Comment comment);

        Task Upvote(Guid id);
        Task Downvote(Guid id);
    }
    public class CommentService : ICommentService
    {
        private HttpHandler HttpHandler;

        public CommentService(HttpHandler httpHandler)
        {
            this.HttpHandler = httpHandler;
        }

        public async Task<Comment> CommentOnPost(Comment comment)
        {
            var m = await HttpHandler.PostJsonAsync<Comment>("Comment/MakeComment", comment);
            return m;
        }

        public async Task<SearchResultDto<Comment>> GetCommentReplies(GetCommentDto filter)
        {
            return await HttpHandler.PostJsonAsync<SearchResultDto<Comment>>("Comment/GetComments", filter);
        }

        public async Task<SearchResultDto<Comment>> GetPostComments(GetCommentDto filter)
        {
            return await HttpHandler.PostJsonAsync<SearchResultDto<Comment>>("Comment/GetComments", filter);
        }

        public async Task ReplyToComment(Comment comment)
        {
            await HttpHandler.PostJsonAsync<Comment>("Comment/MakeComment", comment);
        }

        public async Task Upvote(Guid id)
        {
            await HttpHandler.GetAsync($"Comment/Upvote/{id}");
        }

        public async Task Downvote(Guid id)
        {
            await HttpHandler.GetAsync($"Comment/Downvote/{id}");
        }
    }
}
