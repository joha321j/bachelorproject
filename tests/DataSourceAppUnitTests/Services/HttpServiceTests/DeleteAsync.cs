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

public class DeleteAsync : HttpServiceTestsBase
{
    public DeleteAsync()
    {
        HandlerMock.SetupRequest(HttpMethod.Delete, Client.BaseAddress + "datatype")
            .ReturnsResponse(HttpStatusCode.OK, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
    }
        
    [Fact]
    public async Task SendsRequest()
    {
        await Sut.DeleteAsync(Endpoint);
        await Sut.DeleteAsync<DataSource>(Endpoint);
            
        HandlerMock.VerifyRequest(HttpMethod.Delete, Endpoint, Times.Exactly(2));
    }
}