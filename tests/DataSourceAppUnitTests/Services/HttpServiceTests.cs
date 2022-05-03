using System;
using System.Collections.Generic;
using System.Linq;
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

        _handlerMock.SetupRequest(HttpMethod.Get, _client.BaseAddress + "datatype")
            .ReturnsResponse(JsonSerializer.Serialize(FakeData.DataSources), "application/json");

        var localStorageServiceMock = new Mock<ILocalStorageService>();
        localStorageServiceMock.SetupAllProperties();

        _sut = new HttpService(_client, localStorageServiceMock.Object);
    }

    [Fact]
    public async Task SendsRequest_when_CallingGet()
    {
        var dataSourceTypes = await _sut.GetAsync<List<DataSourceType>>("datatype");

        dataSourceTypes.Should().NotBeNull();
        dataSourceTypes!.Count.Should().Be(1);
        _handlerMock.VerifyRequest(HttpMethod.Get, _client.BaseAddress + "datatype", Times.Exactly(1));
    }

    [Theory]
    [InlineData(nameof(HttpService.GetAsync), "Get", true, null, typeof(List<DataSource>))]
    [InlineData(nameof(HttpService.PostAsync), "Post", false, "test", typeof(List<DataSource>))]
    [InlineData(nameof(HttpService.PostAsync), "Post", true, "test", typeof(List<DataSource>))]
    public async Task SendsRequest_when_CallingMethod(
        string methodName,
        string httpMethod,
        bool isGenericMethod = false,
        object? value = null,
        Type? type = null)
    {
        var method = _sut
            .GetType()
            .GetMethods()
            .First(m => m.IsGenericMethodDefinition == isGenericMethod
                        && m.Name == methodName);

        if (isGenericMethod)
        {
            method = method.MakeGenericMethod(type!);
        }

        var result = Activator.CreateInstance(type);
        
        if (value is null)
        {
             result = (method.Invoke(_sut, new object?[] { _client.BaseAddress + "datatype" })!);
        }
        else
        {
            result = (method.Invoke(_sut, new[] { _client.BaseAddress + "datatype", value })!);
        }

        if (result is Task task)
        {
            await task;
        }
        
        var methodType = typeof(HttpMethod).GetProperty(httpMethod)!.GetValue(typeof(HttpMethod));
        _handlerMock.VerifyRequest((HttpMethod)methodType!, _client.BaseAddress + "datatype", Times.Exactly(1));

        result.Should().BeOfType(type);
    }
}