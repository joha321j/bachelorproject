var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddGraphQLServer();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

app.Run();
