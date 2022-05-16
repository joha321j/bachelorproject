using System.Net;
using System.Text;
using System.Text.Json;
using ApplicationCore.Models;
using Serilog;

namespace DataSourceApp.Services;

public class FakeBackendHandler : HttpClientHandler
{
    private readonly ILocalStorageService _localStorageService;

    public FakeBackendHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return await HandleRouteAsync(request, cancellationToken);
    }

    private async Task<HttpResponseMessage> HandleRouteAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var method = request.Method;
        var path = request.RequestUri?.AbsolutePath;

        return path switch
        {
            "/datasource" when method == HttpMethod.Get => await GetAllDataSourcesAsync(),
            "/datatype" when method == HttpMethod.Get => await GetAllDataSourceTypesAsync(),
            _ => await BaseSendAsync(request, cancellationToken)
        };
    }

    private async Task<HttpResponseMessage> BaseSendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        Log.Warning("Could not find a local fake path corresponding to {Path}... Calling API instead", request.RequestUri?.AbsolutePath);
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<HttpResponseMessage> GetAllDataSourceTypesAsync()
    {
        var types = await _localStorageService.GetItem<List<DataSourceType>>("dataSourceTypes");

        return await OkAsync(types);
    }
    
    private async Task<HttpResponseMessage> GetAllDataSourcesAsync()
    {
        var dataSources = await _localStorageService.GetItem<List<DataSource>>("dataSources");

        return await OkAsync(dataSources);
    }

    private async Task<HttpResponseMessage> OkAsync(object? body = null)
    {
        return await JsonResponseAsync(HttpStatusCode.OK, body ?? new { });
    }

    private static async Task<HttpResponseMessage> JsonResponseAsync(HttpStatusCode statusCode, object content)
    {
        var response = new HttpResponseMessage
        {
            StatusCode = statusCode,
            Content = new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json")
        };

        await Task.Delay(500);
        return response;
    }
}