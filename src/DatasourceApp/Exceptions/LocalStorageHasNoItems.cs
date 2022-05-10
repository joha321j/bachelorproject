using System.Diagnostics.CodeAnalysis;

namespace DataSourceApp.Exceptions;

public class LocalStorageHasNoItems : Exception
{
    public LocalStorageHasNoItems(string key)
    {
        Message = $"The key: '{key}', could not be found in the LocalStorage";
    }

    public override string Message { get; }
    
    public static void ThrowIfNull(
        [NotNull] object? argument, 
        string key)
    {
        if (argument != null)
            return;

        Throw(key);
    }
    
    [DoesNotReturn]
    private static void Throw(string key) => throw new LocalStorageHasNoItems(key);
}