using ApplicationCore.Models;
using DatasourceApp.Services;
using Microsoft.AspNetCore.Components;
using Serilog;

namespace DatasourceApp.Pages;

public partial class Index
{
    private DatasourceType? SelectedDatasourceType { get; set; }

    [Inject]
    private IHttpService Client { get; set; } = null!;
    
    private List<DatasourceType>? DatasourceTypes { get; set; }

    private int SelectedDatasourceTypeId { get; set; }

    private List<InputSection>? InputFieldsFrom { get; set; }

    private List<InputSection>? Fields()
    {
        return DatasourceTypes?.FirstOrDefault(f => f.Id == SelectedDatasourceTypeId)?.Fields;
    }

    private Datasource NewDatasource { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        DatasourceTypes = await Client.GetAsync<List<DatasourceType>>("datatype");
        await base.OnInitializedAsync();
    }

    private static void OnValidSubmit()
    {
        
    }
}