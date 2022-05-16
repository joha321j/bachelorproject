using System;
using System.Net.Http;
using DataSourceApp.Services;
using Moq;

namespace DataSourceAppUnitTests.Services.HttpServiceTests;

public abstract class HttpServiceTestsBase : IDisposable
{
    protected readonly HttpService Sut;
    protected readonly Mock<HttpMessageHandler> HandlerMock;
    protected readonly HttpClient Client;
    protected readonly Mock<ILocalStorageService> LocalStorageMock;
    protected string Endpoint => Client.BaseAddress + "datatype";

    protected HttpServiceTestsBase()
    {
        HandlerMock = new Mock<HttpMessageHandler>();

        Client = new HttpClient(HandlerMock.Object);
        Client.BaseAddress = new Uri("https://www.example.com/");

        LocalStorageMock = new Mock<ILocalStorageService>();

        Sut = new HttpService(Client, LocalStorageMock.Object);
    }

    public void Dispose()
    {
        Client.Dispose();
    }
}