namespace UnSocial.WebApp.Helpers
{
    public class AppState
    {
        public event Action OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }

}
