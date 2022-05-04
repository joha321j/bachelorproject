using System.Net;
using System.Text;
using System.Text.Json;
using ApplicationCore.Models;
using ApplicationCore.Services;
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
            "/datasource" when method == HttpMethod.Get => await GetAllDatasourcesAsync(),
            "/datatype" when method == HttpMethod.Get => await GetAllDatasourceTypesAsync(),
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

    private async Task<HttpResponseMessage> GetAllDatasourceTypesAsync()
    {
        var types = await _localStorageService.GetItem<List<DataSourceType>>("datasourceTypes")
                    ?? FakeData.DataSourceTypes;

        return await OkAsync(types);
    }
    
    private async Task<HttpResponseMessage> GetAllDatasourcesAsync()
    {
        var datasources = await _localStorageService.GetItem<List<DataSource>>("datasources")
                          ?? FakeData.DataSources;

        return await OkAsync(datasources);
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