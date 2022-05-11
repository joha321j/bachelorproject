using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using FluentAssertions;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace DataSourceAppUnitTests.Services.HttpServiceTests;

public class GetAsync : HttpServiceTestsBase
{
    public GetAsync()
    {
        HandlerMock.SetupRequest(HttpMethod.Get, Endpoint)
            .ReturnsResponse(HttpStatusCode.OK, JsonSerializer.Serialize(FakeData.DataSources), "application/json");
    }

    [Fact]
    public async Task ReturnsRequestedData()
    {
        var dataSources = await Sut.GetAsync<List<DataSource>>(Endpoint);
        dataSources.Should().BeOfType<List<DataSource>>();
    }

    [Fact]
    public async Task SendsRequest()
    {
        await Sut.GetAsync<List<DataSource>>(Endpoint);
        HandlerMock.VerifyRequest(HttpMethod.Get, Endpoint, Times.Exactly(1));
    }

    public class ThrowsException : GetAsync
    {
        public ThrowsException()
        {
            HandlerMock.SetupRequest(HttpMethod.Get, Client.BaseAddress + "datatype/badRequest")
                .ReturnsResponse(HttpStatusCode.BadRequest);

            HandlerMock.SetupRequest(HttpMethod.Get, Client.BaseAddress + "datatype/badRequestWithMessage")
                .ReturnsResponse(HttpStatusCode.BadRequest, "{\"message\":\"Bad Request was given\"}");
        }

        [Fact]
        public async Task InvalidCall()
        {

            Func<Task> act = async () =>
                await Sut.GetAsync<List<DataSource>>(Client.BaseAddress + "datatype/badRequest");
            await act
                .Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("Response was: Bad Request");
        }

        [Fact]
        public async Task WithMessage_InvalidCall()
        {

            Func<Task> act = async () =>
                await Sut.GetAsync<List<DataSource>>(Client.BaseAddress + "datatype/badRequestWithMessage");
            await act
                .Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("Bad Request was given");
        }
    }
}