using System.Threading.Tasks;
using DataSourceApp.Services;
using FluentAssertions;
using Microsoft.JSInterop;
using Moq;
using Moq.Protected;
using Xunit;

namespace DataSourceAppUnitTests.Services;

public class LocalStorageServiceTests
{
    private readonly LocalStorageService _sut;

    public LocalStorageServiceTests()
    {
        var runtimeMock = new Mock<IJSRuntime>();

        runtimeMock.Setup(m => m.InvokeAsync<string>("localStorage.getItem", "jwtToken"))
            .Returns(ValueTask.FromResult("fakes"));
        
        runtimeMock.Setup(m => m.InvokeVoidAsync("localStorage.setItem", It.IsAny<string>(), It.IsAny<string>()));
        runtimeMock.Setup(m => m.InvokeVoidAsync("localStorage.removeItem", It.IsAny<string>(), It.IsAny<string>()));

        _sut = new LocalStorageService(runtimeMock.Object);
    }

    [Fact]
    public async Task GetItemReturns_Value()
    {
        var result = await _sut.GetItem<string>("jwtToken");
        result.Should().Be("FAKE-JWT-TOKEN");
    }
}