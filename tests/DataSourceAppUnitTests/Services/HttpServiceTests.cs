using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using DataSourceApp.Services;
using FluentAssertions;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace DataSourceAppUnitTests.Services;

public class HttpServiceTests
{
    private readonly HttpService _sut;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly HttpClient _client;
    private string Endpoint => _client.BaseAddress + "datatype";

    public HttpServiceTests()
    {
        _handlerMock = new Mock<HttpMessageHandler>();

        _client = new HttpClient(_handlerMock.Object);
        _client.BaseAddress = new Uri("https://www.example.com/");

        var localStorageServiceMock = new Mock<ILocalStorageService>();
        localStorageServiceMock.SetupAllProperties();

        localStorageServiceMock.Setup(l => l.GetItem<string>(It.IsAny<string>()))
            .Returns(Task.FromResult("FAKE-JWT-TOKEN"));

        _sut = new HttpService(_client, localStorageServiceMock.Object);
    }

    public class GetAsync : HttpServiceTests
    {
        public GetAsync()
        {
            _handlerMock.SetupRequest(HttpMethod.Get, Endpoint)
                .ReturnsResponse(HttpStatusCode.OK, JsonSerializer.Serialize(FakeData.DataSources), "application/json");
        }
        
        [Fact]
        public async Task ReturnsRequestedData()
        {
            var dataSources = await _sut.GetAsync<List<DataSource>>(Endpoint);
            dataSources.Should().BeOfType<List<DataSource>>();
        }
        
        [Fact]
        public async Task SendsRequest()
        {
            await _sut.GetAsync<List<DataSource>>(Endpoint);
            _handlerMock.VerifyRequest(HttpMethod.Get, Endpoint, Times.Exactly(1));
        }

        public class ThrowsException : GetAsync
        {
            public ThrowsException()
            {
                _handlerMock.SetupRequest(HttpMethod.Get, _client.BaseAddress + "datatype/badRequest")
                    .ReturnsResponse(HttpStatusCode.BadRequest);
        
                _handlerMock.SetupRequest(HttpMethod.Get, _client.BaseAddress + "datatype/badRequestWithMessage")
                    .ReturnsResponse(HttpStatusCode.BadRequest, "{\"message\":\"Bad Request was given\"}");
            }
            
            [Fact]
            public async Task InvalidCall()
            {
        
                Func<Task> act = async() => await _sut.GetAsync<List<DataSource>>(_client.BaseAddress + "datatype/badRequest");
                await act
                    .Should()
                    .ThrowAsync<HttpRequestException>()
                    .WithMessage("Response was: Bad Request");
            }
    
            [Fact]
            public async Task WithMessage_InvalidCall()
            {
        
                Func<Task> act = async() => await _sut.GetAsync<List<DataSource>>(_client.BaseAddress + "datatype/badRequestWithMessage");
                await act
                    .Should()
                    .ThrowAsync<HttpRequestException>()
                    .WithMessage("Bad Request was given");
            }
        }
    }

    public class PostAsync : HttpServiceTests
    {
        public PostAsync()
        {
            _handlerMock.SetupRequest(HttpMethod.Post, Endpoint)
                .ReturnsResponse(HttpStatusCode.Created, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
        }

        [Fact]
        public async Task SendsRequest()
        {
            await _sut.PostAsync(Endpoint, FakeData.DataSources[0]);
            await _sut.PostAsync<DataSource>(Endpoint, FakeData.DataSources[0]);
            
            _handlerMock.VerifyRequest(HttpMethod.Post, Endpoint, Times.Exactly(2));
        }
    }

    public class Put : HttpServiceTests
    {
        public Put()
        {
            _handlerMock.SetupRequest(HttpMethod.Put, Endpoint)
                .ReturnsResponse(HttpStatusCode.Created, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
        }
        
        [Fact]
        public async Task SendsRequest()
        {
            await _sut.PutAsync(Endpoint, FakeData.DataSources[0]);
            await _sut.PutAsync<DataSource>(Endpoint, FakeData.DataSources[0]);
            
            _handlerMock.VerifyRequest(HttpMethod.Put, Endpoint, Times.Exactly(2));
        }
    }

    public class Delete : HttpServiceTests
    {
        public Delete()
        {
            _handlerMock.SetupRequest(HttpMethod.Delete, _client.BaseAddress + "datatype")
                .ReturnsResponse(HttpStatusCode.OK, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
        }
        
        [Fact]
        public async Task SendsRequest()
        {
            await _sut.DeleteAsync(Endpoint);
            await _sut.DeleteAsync<DataSource>(Endpoint);
            
            _handlerMock.VerifyRequest(HttpMethod.Delete, Endpoint, Times.Exactly(2));
        }
    }

    [Fact]
    public async Task AddsJwtToken_when_TokenExists_and_IsApi()
    {
        var request = new HttpRequestMessage();
        request.RequestUri = new Uri("datatype", UriKind.Relative);
        
        await _sut.AddJwtHeaderAsync(request);
        
        request.Headers.Authorization.Should().NotBeNull();
        request.Headers.Authorization!.Parameter.Should().Be("FAKE-JWT-TOKEN");
    }
}