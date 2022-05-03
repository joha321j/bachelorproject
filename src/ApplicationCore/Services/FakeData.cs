using System.Text.Json;
using ApplicationCore.Models;

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

    public static List<Book> Books => new()
    {
        new Book
        {
            Title = "Git Good",
            Author = new Author
            {
                Name = "Mike Meldgaard"
            }
        }
    };

    public static string DatasourcesJsonText => JsonSerializer.Serialize(DataSources);
}