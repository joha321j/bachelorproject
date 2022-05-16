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

public class PutAsync : HttpServiceTestsBase
{
    public PutAsync()
    {
        HandlerMock.SetupRequest(HttpMethod.Put, Endpoint)
            .ReturnsResponse(HttpStatusCode.Created, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
    }
        
    [Fact]
    public async Task SendsRequest()
    {
        await Sut.PutAsync(Endpoint, FakeData.DataSources[0]);
        await Sut.PutAsync<DataSource>(Endpoint, FakeData.DataSources[0]);
            
        HandlerMock.VerifyRequest(HttpMethod.Put, Endpoint, Times.Exactly(2));
    }
}