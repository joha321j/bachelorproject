using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataSourceGraphApiTest;

public class TestClientHandler : HttpClientHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {

        Request = request.RequestUri;
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Accepted,
            Content = new StringContent(
                JsonSerializer.Serialize(string.Empty),
                Encoding.UTF8,
                "application/json")
        });
    }

    public Uri? Request { get; private set; }
}