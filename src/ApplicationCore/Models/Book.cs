namespace DatasourceGraphApi;

public class Book
{
    public string Title { get; init; } = "GraphQL Test"!;

    public Author Author { get; init; } = new Author();
}

public class Author
{
    public string Name { get; init; } = "Johannes Ehlers Nyholm Thomsen";
}