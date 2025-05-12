//using Toxiq.Mobile.Service.OfflineDataService;
using Unsocial.WebApp.Services.OnlineDataService;
using UnSocial.WebApp.Services;
using UnSocial.WebApp.Services.OnlineDataService;

namespace Unsocial.WebApp.Services
{
    public interface IApiService
    {
        IAuthService AuthService { get; set; }
        IColorService ColorService { get; set; }
        IPostService PostService { get; set; }
        IUserService UserService { get; set; }
        ICommentService CommentService { get; set; }
        INotesService NotesService { get; set; }
    }

    public class ApiService : IApiService
    {
        private ITelegramJsInterop TelegramJsInterop;
        private HttpHandler handler;
        public ApiService(ITelegramJsInterop telegramJsInterop, IHttpClientFactory http, TokenService tokenService)
        {

            handler = new HttpHandler(http, tokenService);
            TelegramJsInterop = telegramJsInterop;

            CommentService = new CommentService(handler);
            UserService = new UserService(handler);
            PostService = new PostService(handler);
            ColorService = new ColorService(handler);
            AuthService = new AuthService(TelegramJsInterop, handler);
            NotesService = new NotesService(handler);
        }

        public INotesService NotesService { get; set; }
        public IAuthService AuthService { get; set; }
        public IColorService ColorService { get; set; }
        public IPostService PostService { get; set; }
        public IUserService UserService { get; set; }
        public INotesService NotesService { get; set; }
        public ICommentService CommentService { get; set; }
    }
}
