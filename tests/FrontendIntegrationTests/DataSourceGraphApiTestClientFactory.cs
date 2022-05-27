using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace FrontendIntegrationTests;

public class DataSourceGraphApiTestClientFactory : WebApplicationFactory<DataSourceGraphApi.Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTest");
        return base.CreateHost(builder);
    }
}