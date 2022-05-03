using System.Text.Json;
using ApplicationCore.Models;
using ApplicationCore.Models.AppInsights.Queries;

namespace ApplicationCore.Services;

public static class FakeData
{
    public static List<DataSource> DataSources => new()
    {
        new DataSource
        {
            Name = "Academy App Insights",
            Type = new DataSourceType
            {
                Id = 1,
                Name = "Azure App Insights",
                Fields = new List<InputField>
                {
                    new()
                    {
                        Name = "Api Key",
                        InputType = nameof(String),
                        Required = true
                    }
                }
            }
        }
    };

    public static QueryResults QueryResults => new(new List<Table>
    {
        new("TestTable", 
            new List<Column>
            {
                new(name: "TestColumn", type: "Name")
            },
            new List<IList<string>>
            {
                new List<string>
                {
                    "TestRow"
                },
                new List<string>
                {
                    "BestRow"
                }
            })
    });

    public static string DatasourcesJsonText => JsonSerializer.Serialize(DataSources);
}