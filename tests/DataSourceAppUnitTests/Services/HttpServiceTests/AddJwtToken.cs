using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace DataSourceAppUnitTests.Services.HttpServiceTests;

public class AddJwtToken : HttpServiceTestsBase
{
    public AddJwtToken()
    {
        LocalStorageMock.SetupAllProperties();

        LocalStorageMock.Setup(l => l.GetItem<string>(It.IsAny<string>()))
            .Returns(Task.FromResult("FAKE-JWT-TOKEN"));
    }
    
    [Fact]
    public async Task AddsJwtToken()
    {
        var request = new HttpRequestMessage();
        request.RequestUri = new Uri("datatype", UriKind.Relative);
        
        await Sut.AddJwtHeaderAsync(request);
        
        request.Headers.Authorization.Should().NotBeNull();
        request.Headers.Authorization!.Parameter.Should().Be("FAKE-JWT-TOKEN");
    }
}