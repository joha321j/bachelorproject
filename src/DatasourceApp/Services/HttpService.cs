using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;

namespace DatasourceApp.Services;

public interface IHttpService
{
    Task<T?> Get<T>(string uri);
    
    Task Post(string uri, object value);
    Task<T?> Post<T>(string uri, object value);
    
    Task Put(string uri, object value);
    Task<T?> Put<T>(string uri, object value);
    
    Task Delete(string uri);
    Task<T?> Delete<T>(string uri);
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

    public async Task<T?> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }

    public async Task Post(string uri, object value)
    {
        var request = CreateRequest(HttpMethod.Post, uri, value);
        await SendRequest(request);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string uri, object? value = null)
    {
        var request = new HttpRequestMessage(method, uri);

        if (value is not null)
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

        return request;
    }

    public async Task<T?> Post<T>(string uri, object value)
    {
        var request = CreateRequest(HttpMethod.Post, uri, value);
        return await SendRequest<T>(request);
    }

    public async Task Put(string uri, object value)
    {
        var request = CreateRequest(HttpMethod.Put, uri, value);
        await SendRequest(request);
    }

    public async Task<T?> Put<T>(string uri, object value)
    {
        var request = CreateRequest(HttpMethod.Put, uri, value);
        return await SendRequest<T>(request);
    }

    public async Task Delete(string uri)
    {
        var request = CreateRequest(HttpMethod.Delete, uri);
        await SendRequest(request);
    }

    public async Task<T?> Delete<T>(string uri)
    {
        var request = CreateRequest(HttpMethod.Delete, uri);
        return await SendRequest<T>(request);
    }
    
    private async Task SendRequest(HttpRequestMessage request)
    {
        using var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("account/logout");
            return;
        }

        await HandleErrors(response);
    }

    private async Task<T?> SendRequest<T>(HttpRequestMessage request)
    {
        using var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("account/logout");
            return default;
        }

        await HandleErrors(response);

        var options = new JsonSerializerOptions();
        options.PropertyNameCaseInsensitive = true;
        options.Converters.Add(new JsonStringEnumConverter());

        return await response.Content.ReadFromJsonAsync<T>(options);
    }

    private async Task AddJwtHeader(HttpRequestMessage request)
    {
        var token = await _localStorageService.GetItem<string>("jwtToken");
        var isApiUrl = !request.RequestUri!.IsAbsoluteUri;

        if (string.IsNullOrWhiteSpace(token) && isApiUrl)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    
    private async Task HandleErrors(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error?["message"]);
        }
    }
}