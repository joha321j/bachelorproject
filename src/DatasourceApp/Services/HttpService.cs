using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Serilog;

namespace DataSourceApp.Services;

public interface IHttpService
{
    Task<T?> GetAsync<T>(string uri);
    
    Task PostAsync(string uri, object value);
    Task<T?> PostAsync<T>(string uri, object value);
    
    Task PutAsync(string uri, object value);
    Task<T?> PutAsync<T>(string uri, object value);
    
    Task DeleteAsync(string uri);
    Task<T?> DeleteAsync<T>(string uri);
}

public class HttpService : IHttpService
{
    private HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    private IConfiguration _configuration;

    public HttpService(
        HttpClient httpClient, 
        NavigationManager navigationManager, 
        ILocalStorageService localStorageService, 
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _configuration = configuration;
    }

    public async Task<T?> GetAsync<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequestAsync<T>(request);
    }

    public async Task PostAsync(string uri, object value)
    {
        var request = CreateRequest(HttpMethod.Post, uri, value);
        await SendRequestAsync(request);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string uri, object? value = null)
    {
        var request = new HttpRequestMessage(method, uri);

        if (value is not null)
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

        return request;
    }

    public async Task<T?> PostAsync<T>(string uri, object value)
    {
        var request = CreateRequest(HttpMethod.Post, uri, value);
        return await SendRequestAsync<T>(request);
    }

    public async Task PutAsync(string uri, object value)
    {
        var request = CreateRequest(HttpMethod.Put, uri, value);
        await SendRequestAsync(request);
    }

    public async Task<T?> PutAsync<T>(string uri, object value)
    {
        var request = CreateRequest(HttpMethod.Put, uri, value);
        return await SendRequestAsync<T>(request);
    }

    public async Task DeleteAsync(string uri)
    {
        var request = CreateRequest(HttpMethod.Delete, uri);
        await SendRequestAsync(request);
    }

    public async Task<T?> DeleteAsync<T>(string uri)
    {
        var request = CreateRequest(HttpMethod.Delete, uri);
        return await SendRequestAsync<T>(request);
    }
    
    private async Task SendRequestAsync(HttpRequestMessage request)
    {
        using var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("account/logout");
            return;
        }

        await HandleErrorsAsync(response);
    }

    private async Task<T?> SendRequestAsync<T>(HttpRequestMessage request)
    {
        Log.Information("Sending HttpRequestMessage: {@Request}", request);
        
        using var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("account/logout");
            return default;
        }

        await HandleErrorsAsync(response);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        Log.Information("Received HttpResponseMessage: {@Response}", response);
        
        return await response.Content.ReadFromJsonAsync<T>(options);
    }

    private async Task AddJwtHeaderAsync(HttpRequestMessage request)
    {
        var token = await _localStorageService.GetItem<string>("jwtToken");
        var isApiUrl = !request.RequestUri!.IsAbsoluteUri;

        if (string.IsNullOrWhiteSpace(token) && isApiUrl)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    
    private static async Task HandleErrorsAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error?["message"]);
        }
    }
}