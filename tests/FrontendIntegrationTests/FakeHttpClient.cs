using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataSourceGraphApiIntegrationTests;

public class FakeHttpClient : HttpClient
{
    private HttpResponseMessage _responseMessage;
    
    public FakeHttpClient() : this(new HttpResponseMessage())
    {
        BaseAddress = new Uri("http://fake-url/");
    }
    
    public FakeHttpClient(object response)
    {
        SetStringContentResponse(response);
        BaseAddress = new Uri("http://fake-url/");
    }

    public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_responseMessage);
    }

    public HttpResponseMessage SetStringContentResponse(object response)
    {
        _responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        _responseMessage.Content = new StringContent(JsonSerializer.Serialize(response));
        return _responseMessage;
    }
}