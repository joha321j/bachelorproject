using ApplicationCore.Models;
using DatasourceApp.Services;
using Microsoft.AspNetCore.Components;

namespace DatasourceApp.Pages;

public partial class Index
{
    private DatasourceType? SelectedDatasourceType => DatasourceTypes.FirstOrDefault(d => d.Id == SelectedDatasourceTypeId);

    [Inject]
    private IHttpService Client { get; set; } = null!;

    private List<DatasourceType> DatasourceTypes { get; set; } = new();

    private int SelectedDatasourceTypeId { get; set; }

    private Datasource _newDatasource = new();

    protected override async Task OnInitializedAsync()
    {
        DatasourceTypes = await Client.GetAsync<List<DatasourceType>>("datatype")
                          ?? throw new InvalidOperationException();
        
        await base.OnInitializedAsync();
    }

    private static void OnValidSubmit()
    {
        
    }
}