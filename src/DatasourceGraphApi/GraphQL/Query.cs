namespace DatasourceGraphApi.GraphQL;

public class Query
{
    private readonly ResolverClient _resolverClient;

    public Query(ResolverClient resolverClient)
    {
        _resolverClient = resolverClient;
    }

    public Book GetBook() => _resolverClient.Resolve<Book>("", "");
}   