using System.Net;
using System.Text;
using System.Text.Json;
using ApplicationCore.Services;

namespace DataFakingLibrary;

public class FakeYoutubeHandler : HttpClientHandler
{
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

        var paths = path?.Split('/');

        if (paths == null ||
            method != HttpMethod.Get ||
            !paths[1].Equals("youtube") ||
            !paths[2].Equals("v3"))
            return await base.SendAsync(request, cancellationToken);

        return paths[4] switch
        {
            "searchResults" => await GetSearchResultData(),
            _ => await base.SendAsync(request, cancellationToken)
        };
    }
    
    private async Task<HttpResponseMessage> GetSearchResultData()
    {
        var data = FakeData.SearchResults;

        return await Ok(data);
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