using ApplicationCore.Models;
using DatasourceApp.Services;
using Microsoft.AspNetCore.Components;

namespace DatasourceApp.Pages;

public partial class Index
{
    private DatasourceType? _selectedDatasourceType;
    private DatasourceType? SelectedDatasourceType
    {
        get
        {
            return DatasourceTypes.FirstOrDefault(d => d.Id == SelectedDatasourceTypeId);
        }
        set => _selectedDatasourceType = value;
    }

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