﻿namespace DatasourceGraphApi.GraphQL;

public class Query
{
    private readonly ResolverClient _client;

    public Query(ResolverClient client)
    {
        _client = client;
    }

    public async Task<List<Book>?> GetBooks()
    {
        return await _client.Resolve<List<Book>>("abetauh", "atoeuha");
    }
}   