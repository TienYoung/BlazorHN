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

        public async Task<List<int>> GetTopStoryIdListAsync()
        { 
            var idList = await _httpClient.GetFromJsonAsync<List<int>>($"topstories.json");
            return idList ?? new List<int>();
        }

        public async Task<HackerNewsItem?> GetItemAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<HackerNewsItem>($"item/{id}.json");
        }
    }
}
