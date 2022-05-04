using ApplicationCore.Models;
using DataSourceApp.Services;
using Microsoft.AspNetCore.Components;

namespace DataSourceApp.Pages;

public partial class Index : ComponentBase
{
    private DataSourceType? SelectedDataSourceType => DataSourceTypes.FirstOrDefault(d => d.Id == SelectedDatasourceTypeId);

    [Inject]
    private IHttpService Client { get; set; } = null!;

    private List<DataSourceType> DataSourceTypes { get; set; } = new();

    private int SelectedDatasourceTypeId { get; set; }

    private DataSource _newDataSource = new();

    protected override async Task OnInitializedAsync()
    {
        DataSourceTypes = await Client.GetAsync<List<DataSourceType>>("datatype")
                          ?? throw new InvalidOperationException();
        
        await base.OnInitializedAsync();
    }

    private static void OnValidSubmit()
    {
        
    }
}