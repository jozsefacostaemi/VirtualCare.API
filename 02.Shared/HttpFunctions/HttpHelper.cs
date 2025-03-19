
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace HttpFunctions;
public static class HttpClientHelper
{
    private static readonly HttpClient _httpClient = new HttpClient();

    static HttpClientHelper()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static async Task<TResponse> GetAsync<TResponse>(string requestUri, string? baseAddress = null)
    {
        if (!string.IsNullOrEmpty(baseAddress))
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        var response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(content)
    ?? throw new JsonException($"Error deserializing response from {requestUri}");
    }

    public static async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUri, TRequest content, string? baseAddress = null)
    {
        if (!string.IsNullOrEmpty(baseAddress))
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        var jsonContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(requestUri, jsonContent);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(responseContent)
     ?? throw new JsonException($"Error deserializing response from {requestUri}");
    }

    public static async Task<TResponse> PutAsync<TRequest, TResponse>(string requestUri, TRequest content, string? baseAddress = null)
    {
        if (!string.IsNullOrEmpty(baseAddress))
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        var jsonContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(requestUri, jsonContent);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(responseContent)
     ?? throw new JsonException($"Error deserializing response from {requestUri}");
    }

    public static async Task DeleteAsync(string requestUri, string? baseAddress = null)
    {
        if (!string.IsNullOrEmpty(baseAddress))
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        var response = await _httpClient.DeleteAsync(requestUri);
        response.EnsureSuccessStatusCode();
    }
}
