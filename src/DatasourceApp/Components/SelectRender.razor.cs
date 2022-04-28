using ApplicationCore.Models;
using Microsoft.AspNetCore.Components;

namespace DatasourceApp.Components;

public partial class SelectRender<T>
    where T : InputTypeSelection
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public List<T>? InputCollection { get; set; }
    
    [Parameter]
    public string? PlaceholderText { get; set; }

    [Parameter]
    public string Id { get; set; } = null!;

    private int _selectedId;
    private int SelectedId
    {
        get => _selectedId;
        set
        {
            var newValue = InputCollection?.FirstOrDefault(i => i.Id == value);
            Value = newValue
                    ?? throw new InvalidOperationException();
        }
        
    }

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