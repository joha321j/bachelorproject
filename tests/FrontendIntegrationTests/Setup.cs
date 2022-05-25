using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace DataSourceGraphApiIntegrationTests;

public class Setup
{
    protected readonly HttpClient Client;

    protected Setup(Func<IServiceCollection, IServiceCollection> collection)
    {
        var application = new TestClient(collection);
        using var scope = application.Services.CreateScope();
        Client = application.CreateClient();
    }
}