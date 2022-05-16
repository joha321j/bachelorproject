using System.Text.Json;
using System.Text.Json.Nodes;
using DataSourceApp.Exceptions;
using Microsoft.JSInterop;

namespace DataSourceApp.Services;

public interface ILocalStorageService
{
    Task<T> GetItem<T>(string key);
    Task SetItem(string key, object value);
    Task RemoveItem(string key);
}

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T> GetItem<T>(string key)
    {
        var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
        LocalStorageHasNoItems.ThrowIfNull(json, key);

        var jsonObject = JsonSerializer.Deserialize<JsonObject>(json)!;

        var result = jsonObject[key];
        LocalStorageItemNotFoundException.ThrowIfNull(result, key);

        return result.GetValue<T>();

    }
    
    public async Task SetItem(string key, object value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
    }

    public async Task RemoveItem(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}