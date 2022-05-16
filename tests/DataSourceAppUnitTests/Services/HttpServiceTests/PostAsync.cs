using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace DataSourceAppUnitTests.Services.HttpServiceTests;

public class PostAsync : HttpServiceTestsBase
{
    public PostAsync()
    {
        HandlerMock.SetupRequest(HttpMethod.Post, Endpoint)
            .ReturnsResponse(HttpStatusCode.Created, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
    }

    [Fact]
    public async Task SendsRequest()
    {
        await Sut.PostAsync(Endpoint, FakeData.DataSources[0]);
        await Sut.PostAsync<DataSource>(Endpoint, FakeData.DataSources[0]);
            
        HandlerMock.VerifyRequest(HttpMethod.Post, Endpoint, Times.Exactly(2));
    }
}