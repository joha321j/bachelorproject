using ApplicationCore.Models;
using DatasourceApp.Services;
using Microsoft.AspNetCore.Components;

namespace DatasourceApp.Pages;

public partial class Index
{
    private DatasourceTypeSelection? SelectedDatasourceType =>
        DatasourceTypes.FirstOrDefault(d => d.Id == SelectedDatasourceTypeId);

    [Inject]
    private IHttpService Client { get; set; } = null!;
    
    private List<DatasourceTypeSelection>? DatasourceTypes { get; set; }

    private int SelectedDatasourceTypeId { get; set; }

    private List<InputSection>? InputFieldsFrom { get; set; }

    private List<InputSection>? Fields()
    {
        return DatasourceTypes?.FirstOrDefault(f => f.Id == SelectedDatasourceTypeId)?.Fields;
    }

    private Datasource NewDatasource { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        DatasourceTypes = await Client.GetAsync<List<DatasourceTypeSelection>>("datatype");
        await base.OnInitializedAsync();
    }

    private static void OnValidSubmit()
    {
        
    }
}