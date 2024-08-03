using Toxiq.Mobile.Dto;
using UnSocial.WebApp.Services;

namespace Unsocial.WebApp.Services.OnlineDataService
{
    public interface IColorService
    {
        Task<List<ColorListDto>> GetColors();
        Task SuggestColor(string hex);
    }

    public class ColorService : IColorService
    {
        private HttpHandler HttpHandler;

        public ColorService(HttpHandler httpHandler)
        {

            HttpHandler = httpHandler;
        }

        public async Task<List<ColorListDto>> GetColors()
        {
            return await HttpHandler.GetJsonAsync<List<ColorListDto>>("Color/DebugColorList");
        }

        public async Task SuggestColor(string hex)
        {
            await HttpHandler.GetAsync($"Color/SuggestColor/{hex}");
        }
    }
}
