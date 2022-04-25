using ApplicationCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace DatasourceApp.Components;

public class Fields : ComponentBase
{
    [Parameter]
    public List<InputField>? InputFields { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (InputFields is null) return;
        
        var seq = 0;
        builder.OpenElement(++seq, "div");

        foreach (var field in InputFields)
        {
            AddInputs(ref builder, ref seq, field);
        }
        
        builder.CloseElement();
    }

    private void AddInputs(ref RenderTreeBuilder builder, ref int seq, InputField inputField)
    {
        builder.OpenElement(++seq, "label");
        builder.AddContent(++seq, inputField.Name);
        builder.CloseElement();
        
        builder.OpenElement(++seq, "input");
        builder.AddAttribute(++seq, "class", "field border-primary p-4");
        builder.AddContent(++seq, inputField.Value);
        
        builder.CloseElement();
    }
}