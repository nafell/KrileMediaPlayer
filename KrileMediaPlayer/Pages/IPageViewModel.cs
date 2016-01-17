namespace KrileMediaPlayer.Pages
{
    public interface IPageViewModel
    {
        int InitialFetchPercentage { get; set; }
        string Title { get; set; }
        bool IsSelected { get; set; }
        string Url { get; set; }
    }
}
