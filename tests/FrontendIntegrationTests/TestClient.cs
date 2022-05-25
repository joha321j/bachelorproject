using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataSourceGraphApiIntegrationTests;

public class TestClient : WebApplicationFactory<DataSourceApp.Program>
{
    private readonly Func<IServiceCollection, IServiceCollection> _setupMockServices;

    public TestClient(Func<IServiceCollection, IServiceCollection> setupMockServices)
    {
        _setupMockServices = setupMockServices;
    }
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services = _setupMockServices(services);

        });

        return base.CreateHost(builder);
    }
}