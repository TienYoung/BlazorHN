using BlazorHN.Models;

namespace BlazorHN.Services
{
    public interface IHackerNewsService
    {
        Task<List<int>> GetTopStoryIdListAsync();
        Task<List<int>> GetStoryIdListAsync(string type);
        Task<HackerNewsItem?> GetItemAsync(int id);
    }
}
