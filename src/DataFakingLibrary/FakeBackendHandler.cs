using System.Net;
using System.Text;
using System.Text.Json;
using ApplicationCore.Models;
using ApplicationCore.Services;
using DatasourceApp.Services;

namespace DataFakingLibrary;

public class FakeBackendHandler : HttpClientHandler
{
    private readonly ILocalStorageService _localStorageService;

    private List<DataSource> _dataSources;

    public FakeBackendHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        _dataSources = new List<DataSource>();
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return await HandleRoute(request, cancellationToken);
    }

    private async Task<HttpResponseMessage> HandleRoute(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var method = request.Method;
        var path = request.RequestUri?.AbsolutePath;

        if (path == "datasource" && method == HttpMethod.Get)
            return await GetAllDataSources();

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<HttpResponseMessage> GetAllDataSources()
    {
        var dataSources = await _localStorageService.GetItem<List<DataSource>>("datasources")
                          ?? FakeData.Datasources;

        return await Ok(dataSources);
    }

    private async Task<HttpResponseMessage> Ok(object? body = null)
    {
        return await JsonResponse(HttpStatusCode.OK, body ?? new { });
    }

    private async Task<HttpResponseMessage> JsonResponse(HttpStatusCode statusCode, object content)
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