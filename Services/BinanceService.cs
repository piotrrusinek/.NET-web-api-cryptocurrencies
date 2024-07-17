using CryptoCurrencies.Models;

using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CryptoCurrencies.Services;

public class BinanceService
{
    private readonly HttpClient _client;

    public BinanceService()
    {
        HttpClientHandler handler = new HttpClientHandler 
        { 
            AutomaticDecompression = System.Net.DecompressionMethods.All 
        };
        
        _client = new HttpClient();
    }

    public async Task<string> GetAsync(string uri)
    {
        using HttpResponseMessage response = await _client.GetAsync(uri);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PostAsync(string uri, string data, string contentType)
    {
        using HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, contentType);
        
        HttpRequestMessage requestMessage = new HttpRequestMessage() 
        { 
            Content = content,
            Method = HttpMethod.Post,
            RequestUri = new Uri(uri)
        };
        
        using HttpResponseMessage response = await _client.SendAsync(requestMessage);

        return await response.Content.ReadAsStringAsync();
    }
}