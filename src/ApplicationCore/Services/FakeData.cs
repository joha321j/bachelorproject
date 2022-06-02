using ApplicationCore.Models;
using ApplicationCore.Models.AppInsights.Events;
using ApplicationCore.Models.AppInsights.Metrics;
using ApplicationCore.Models.AppInsights.Queries;
using ApplicationCore.Models.Youtube;

namespace ApplicationCore.Services;

public static class FakeData
{
    public static List<DataSource> DataSources => new()
    {
        new DataSource
        {
            Name = "Academy App Insights",
            DataSourceType = new DataSourceType
            {
                Id = 1,
                Name = "Azure App Insights",
                Fields = new List<InputSection>
                {
                    new()
                    {
                        Name = "Authorization",
                        ChoicesName = "DataSourceType",
                        Required = true,
                        Choices = new List<InputChoice>
                        {
                            new()
                            {
                                Id = 1,
                                Name = "Api Key",
                                Fields = new List<InputField>
                                {
                                    new()
                                    {
                                        Name = "Api Key",
                                        InputType = "text",
                                        Required = true
                                    }
                                }
                            },
                            new()
                            {
                                Id = 2,
                                Name = "Username/Password",
                                Fields = new List<InputField>
                                {
                                    new()
                                    {
                                        Name = "Username",
                                        InputType = "text",
                                        Required = true
                                    },
                                    new()
                                    {
                                        Name = "Password",
                                        InputType = "password",
                                        Required = true
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    };

    public static QueryResults QueryResults => new()
    {
        Tables = new List<Table>
        {
            new(){
                Name = "TestTable",
                Columns = new List<Column>
                {
                    new()
                    {
                        Name = "TestColumn",
                        Type = "Name"
                    }
                },
                Rows = new List<IList<string>>
                {
                    new List<string>
                    {
                        "TestRow"
                    },
                    new List<string>
                    {
                        "BestRow"
                    }
                }}
        }
    };

    public static MetricsResult MetricsResults => new()
    {
        Value = new MetricsResultInfo()
    };

    public static EventsResults EventsResults => new()
    {

    };

    public static List<DataSourceType> DataSourceTypes => DataSources
        .Select(d => d.DataSourceType)
        .DistinctBy(d => d.Name)
        .ToList();

    public static List<Search> Searches => new()
    {
        new Search
        {
            Etag = "sadasd",
            Id = new Id
            {
                ChannelId = "sadad",
                Kind = "testKind",
                PlaylistId = "asdasd",
                VideoId = "asdasd"
            },
            Kind = "TestKind",
            Snippet = new Snippet
            {
                ChannelId = "asdsda",
                ChannelTitle = "sadsdas",
                Description = "asdasd",
                LiveBroadcastContent = "asdasd",
                PublishedAt = "asdasd",
                Thumbnails = new Thumbnails
                {
                    Height = 10,
                    Url = "sadasd",
                    Width = 20
                },
                Title = "asdasdas"
            }
        },
        new Search
        {
            Etag = "234567",
            Id = new Id
            {
                ChannelId = "45",
                Kind = "56",
                PlaylistId = "4567",
                VideoId = "4567"
            },
            Kind = "TestKind2",
            Snippet = new Snippet
            {
                ChannelId = "4567",
                ChannelTitle = "4567",
                Description = "4567",
                LiveBroadcastContent = "4567",
                PublishedAt = "456",
                Thumbnails = new Thumbnails
                {
                    Height = 10,
                    Url = "5678",
                    Width = 20
                },
                Title = "4567"
            }
        }
            
    };
}