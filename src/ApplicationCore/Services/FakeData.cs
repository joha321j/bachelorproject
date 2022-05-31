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

    public static QueryResults QueryResults => new(new List<Table>
    {
        new("TestTable", 
            new List<Column>
            {
                new("TestColumn", "Name")
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

    public static List<Comments> Comments => new(new List<Comments>
    {
        new()
        {
            Id = 1,
            Kind = "Sport",
            Snippet = new Snippet
            {
                AuthorChannelId = new AuthorChannelId
                {
                    Value = 1
                },
                AuthorChannelUrl = "AuthorChannelTestUrl",
                AuthorDisplayName = "NewAuthor",
                AuthorProfileImageUrl = "TestImageUrl",
                CanRate = true,
                ChannelId = 1,
                LikeCount = 2000,
                ModerationStatus = "Modded",
                ParentId = 1,
                PublishedAt = "2015-06-02 23:33:90",
                UpdatedAt = "2015-06-02 23:33:90"
            }

        },
        new()
        {
            Id = 2,
            Kind = "Art",
            Snippet = new Snippet
            {
                AuthorChannelId = new AuthorChannelId
                {
                    Value = 2
                },
                AuthorChannelUrl = "AuthorChannelTestUrl2",
                AuthorDisplayName = "NewAuthor2",
                AuthorProfileImageUrl = "2",
                CanRate = true,
                ChannelId = 2,
                LikeCount = 50,
                ModerationStatus = "Modded2",
                ParentId = 2,
                PublishedAt = "2015-06-02 23:33:90",
                UpdatedAt = "2015-06-02 23:33:90"
            }
        }
    });
}