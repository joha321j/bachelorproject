using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Bunit;
using DataSourceApp.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Index = DataSourceApp.Pages.Index;

namespace DataSourceAppUnitTests.Pages;

public class IndexTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    private readonly TestContext _ctx;
    private readonly IRenderedComponent<Index> _cut;

    public IndexTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _ctx = new TestContext();

        var clientMock = new Mock<IHttpService>();

        clientMock.Setup(m => m.GetAsync<List<DataSourceType>>("datatype"))
            .Returns(Task.FromResult(FakeData.DataSourceTypes)!);

        _ctx.Services.AddSingleton(clientMock.Object);

        _cut = _ctx.RenderComponent<Index>();
    }

    [Fact]
    public void Renders_DataSourceTypes_AsOptions()
    { 
        var options = _cut.FindAll("option");

        _cut.RenderCount.Should().Be(1);
        options.Count.Should().Be(FakeData.DataSourceTypes.Count + 1);
        options[0].InnerHtml.Should().Be("Select a datasource type...");
        options[1].InnerHtml.Should().Be("Azure App Insights");
    }

    public void Dispose()
    {
        _ctx.Dispose();
        _cut.Dispose();
    }
}