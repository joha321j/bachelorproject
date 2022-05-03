using ApplicationCore.Models;

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

    public static List<DataSourceType> DataSourceTypes => DataSources
        .Select(d => d.DataSourceType)
        .DistinctBy(d => d.Name)
        .ToList();
}