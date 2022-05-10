using System;
using System.Collections.Generic;
using System.Linq;
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

    public HttpServiceTests()
    {
        _handlerMock = new Mock<HttpMessageHandler>();

        _client = new HttpClient(_handlerMock.Object);
        _client.BaseAddress = new Uri("https://www.example.com/");
        
        SetupRequests();

        var localStorageServiceMock = new Mock<ILocalStorageService>();
        localStorageServiceMock.SetupAllProperties();

        localStorageServiceMock.Setup(l => l.GetItem<string>(It.IsAny<string>()))
            .Returns(Task.FromResult("FAKE-JWT-TOKEN")!);

        _sut = new HttpService(_client, localStorageServiceMock.Object);
    }

    private void SetupRequests()
    {
        _handlerMock.SetupRequest(HttpMethod.Get, _client.BaseAddress + "datatype")
            .ReturnsResponse(HttpStatusCode.OK, JsonSerializer.Serialize(FakeData.DataSources), "application/json");
        
        _handlerMock.SetupRequest(HttpMethod.Post, _client.BaseAddress + "datatype")
            .ReturnsResponse(HttpStatusCode.Created, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
        
        _handlerMock.SetupRequest(HttpMethod.Put, _client.BaseAddress + "datatype")
            .ReturnsResponse(HttpStatusCode.Created, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
        
        _handlerMock.SetupRequest(HttpMethod.Delete, _client.BaseAddress + "datatype")
            .ReturnsResponse(HttpStatusCode.OK, JsonSerializer.Serialize(FakeData.DataSources[0]), "application/json");
        
        _handlerMock.SetupRequest(HttpMethod.Get, _client.BaseAddress + "datatype/badRequest")
            .ReturnsResponse(HttpStatusCode.BadRequest);
        
        _handlerMock.SetupRequest(HttpMethod.Get, _client.BaseAddress + "datatype/badRequestWithMessage")
            .ReturnsResponse(HttpStatusCode.BadRequest, "{\"message\":\"Bad Request was given\"}");
    }

    [Fact]
    public async Task SendsRequest_when_CallingGet()
    {
        var dataSourceTypes = await _sut.GetAsync<List<DataSourceType>>("datatype");

        dataSourceTypes.Should().NotBeNull();
        dataSourceTypes!.Count.Should().Be(1);
        _handlerMock.VerifyRequest(HttpMethod.Get, _client.BaseAddress + "datatype", Times.Exactly(1));
    }

    public class SendsRequest
    {
        [Fact]
        public void when_CallingMethod()
        {
            
        }
    }

    [Theory]
    [InlineData(nameof(HttpService.GetAsync), "Get", true, false, typeof(List<DataSource>))]
    [InlineData(nameof(HttpService.PostAsync), "Post", false, true, null)]
    [InlineData(nameof(HttpService.PostAsync), "Post", true, true, typeof(DataSource))]
    [InlineData(nameof(HttpService.PutAsync), "Put", false, true, null)]
    [InlineData(nameof(HttpService.PutAsync), "Put", true, true, typeof(DataSource))]
    [InlineData(nameof(HttpService.DeleteAsync), "Delete", false, false, null)]
    [InlineData(nameof(HttpService.DeleteAsync), "Delete", true, false, typeof(DataSource))]
    public async Task SendsRequest_when_CallingMethod(
        string methodName,
        string httpMethod,
        bool isGenericMethod = false,
        bool sendFakeObject = false,
        Type? returnType = null)
    {
        var method = _sut
            .GetType()
            .GetMethods()
            .First(m => m.IsGenericMethodDefinition == isGenericMethod
                        && m.Name == methodName);
        
        if (isGenericMethod)
        {
            method = method.MakeGenericMethod(returnType!);
        }

        Task? result;
        
        if (sendFakeObject)
        {
            var valueObj = FakeData.DataSources[0];
            var value = JsonSerializer.Serialize(valueObj);
            result = (Task) method.Invoke(_sut, new object?[] { _client.BaseAddress + "datatype", value })!;
        }
        else
        {
            result = (Task) method.Invoke(_sut, new object?[] { _client.BaseAddress + "datatype" })!;
        }

        await result!.ConfigureAwait(false);
        var resultProp = result.GetType().GetProperty("Result");
        var resultValue = resultProp?.GetValue(result);
        
        var methodType = typeof(HttpMethod).GetProperty(httpMethod)!.GetValue(typeof(HttpMethod));
        _handlerMock.VerifyRequest((HttpMethod)methodType!, _client.BaseAddress + "datatype", Times.Exactly(1));

        if (returnType is not null)
        {
            resultValue.Should().BeOfType(returnType);
        }
    }

    [Fact]
    public async Task ThrowsException_when_InvalidCall()
    {
        
        Func<Task> act = async() => await _sut.GetAsync<List<DataSource>>(_client.BaseAddress + "datatype/badRequest");
        await act
            .Should()
            .ThrowAsync<HttpRequestException>()
            .WithMessage("Response was: Bad Request");
    }
    
    [Fact]
    public async Task ThrowsExceptionWithMessage_when_InvalidCall()
    {
        
        Func<Task> act = async() => await _sut.GetAsync<List<DataSource>>(_client.BaseAddress + "datatype/badRequestWithMessage");
        await act
            .Should()
            .ThrowAsync<HttpRequestException>()
            .WithMessage("Bad Request was given");
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