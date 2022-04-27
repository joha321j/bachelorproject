using Microsoft.AspNetCore.Components;

namespace DatasourceApp.Components;

public partial class SelectRender<T>
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public List<T>? InputCollection { get; set; }
    
    [Parameter]
    public string? PlaceholderText { get; set; }

    [Parameter]
    public string Id { get; set; } = null!;

    private int SelectedId { get; set; }

    [Parameter]
    public T Value
    {
        get => _value;
        set
        {
            if (_value == value) return;

            _value = value;
            ValueChanged.InvokeAsync(value);
        }
        
    }

    private T _value = null!;
    
    [Parameter]
    public EventCallback<T> ValueChanged { get; set; }
}