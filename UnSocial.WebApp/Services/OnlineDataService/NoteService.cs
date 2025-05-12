using Toxiq.Mobile.Dto;

namespace UnSocial.WebApp.Services.OnlineDataService
{
    public interface INotesService
    {
        Task<List<NoteDto>> GetMyNotes();
        Task<NoteDto> GetNote(Guid id);
        Task<HttpResponseMessage> SendNote(NoteDto input);
        Task RespondToNote(BasePost input);
    }

    public class NotesService : INotesService
    {
        private HttpHandler HttpHandler;
        public NotesService(HttpHandler httpHandler)
        {
            this.HttpHandler = httpHandler;
        }

        public async Task<List<NoteDto>> GetMyNotes()
        {
            return await HttpHandler.GetJsonAsync<List<NoteDto>>($"Notes/GetMyNotes");
        }

        public async Task<NoteDto> GetNote(Guid id)
        {
            return await HttpHandler.GetJsonAsync<NoteDto>($"Notes/GetNote/{id}");
        }

        public async Task<HttpResponseMessage> SendNote(NoteDto input)
        {
            return await HttpHandler.PostAsync("Notes/SendNote", input);

        }

        public async Task RespondToNote(BasePost input)
        {
            await HttpHandler.PostJsonAsync<BasePost>("Notes/RespondToNote", input);
        }
    }
}
