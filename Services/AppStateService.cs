namespace BlazorHN.Services
{
    public class AppStateService
    {
        public int? SelectedStoryId { get; private set; }
        public bool IsWide { get; private set; }

        public event Action? OnChange;

        public void SelectStory(int id)
        {
            SelectedStoryId = id;
            OnChange?.Invoke();
        }

        public void ClearStory()
        {
            SelectedStoryId = null;
            OnChange?.Invoke();
        }

        public void SetIsWide(bool wide)
        {
            IsWide = wide;
            OnChange?.Invoke();
        }
    }
}
