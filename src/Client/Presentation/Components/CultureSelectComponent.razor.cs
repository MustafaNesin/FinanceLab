using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Components;

public partial class CultureSelectComponent
{
    private string _cultureLabel = default!;

    [Inject]
    private ISyncLocalStorageService LocalStorage { get; set; } = default!;

    protected override void OnInitialized()
    {
        _cultureLabel = LocalStorage.GetItemAsString("Culture") == "tr-TR" ? "Türkçe" : "English";
    }

    private void ChangeCulture(string value)
    {
        LocalStorage.SetItemAsString("Culture", value);
        NavigationManager.NavigateTo(NavigationManager.Uri, true);
    }
}