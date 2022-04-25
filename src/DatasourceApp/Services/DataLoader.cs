using System.Text.Json;
using ApplicationCore;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Components;
using Serilog;

namespace DatasourceApp.Services;

public interface IDatasourceFetcher
{
    Task<List<Datasource>?> FetchAll();

    Task<List<DatasourceType>?> FetchAllTypes();
}

public class FakeDatasourceFetcher : IDatasourceFetcher 
{
    public async Task<List<Datasource>?> FetchAll()
    {
        var datasources = await Task.FromResult(FakeData.Datasources);
        Log.Information("Datasources: {@Datasources}", datasources);

        return datasources;
        // return JsonSerializer.Deserialize<List<Datasource>>(FakeData.DatasourcesJsonText);
    }

    public async Task<List<DatasourceType>?> FetchAllTypes()
    {
        var datasources = await FetchAll();

        var datasourceTypes = datasources
            .GroupBy(d => d.Name)
            .Select(g => g.First())
            .Select( p => p.Type)
            .ToList();

        // var datasourceTypes = (from dbo in datasources 
        //         select dbo.Type)
        //     .Distinct() as List<DatasourceType>;

        Log.Information("DatasourceTypes: {@DatasourceTypes}", datasourceTypes);

        return await Task.FromResult(datasourceTypes);
    }
}