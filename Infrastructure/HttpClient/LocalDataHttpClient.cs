#region

using System.Net.Http.Json;
using Application.Constants;

#endregion

namespace Infrastructure.HttpClient;

public class LocalDataHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public LocalDataHttpClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int[]> GetWorkingHours(Country country)
    {
        return await _httpClient.GetFromJsonAsync<int[]>($"data/WorkingHours.{country.ToString()}.json") ?? Array.Empty<int>();
    }
}