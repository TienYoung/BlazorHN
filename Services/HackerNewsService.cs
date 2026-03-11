using BlazorHN.Models;
using System.Net.Http.Json;

namespace BlazorHN.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;

        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<int>> GetTopStoryIdListAsync() => GetStoryIdListAsync("topstories");

        public async Task<List<int>> GetStoryIdListAsync(string type)
        {
            var idList = await _httpClient.GetFromJsonAsync<List<int>>($"{type}.json");
            return idList ?? new List<int>();
        }

        public async Task<HackerNewsItem?> GetItemAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<HackerNewsItem>($"item/{id}.json");
        }
    }
}
