using BlazorHN.Models;

namespace BlazorHN.Services
{
    public interface IHackerNewsService
    {
        Task<List<int>> GetTopStoryIdListAsync();
        Task<HackerNewsItem?> GetItemAsync(int id);
    }
}
