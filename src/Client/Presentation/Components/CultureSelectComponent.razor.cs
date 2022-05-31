using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Components;

public partial class CultureSelectComponent
{
    [Inject]
    private ISyncLocalStorageService LocalStorage { get; set; } = default!;

    public string currLang { get; set; } = default!;
    public string currLangTx { get; set; } = default!;
    protected override void OnInitialized()
    {
        currLang = LocalStorage.GetItemAsString("Culture");
        if (currLang == "en-US")
            currLangTx = "English";
        else if (currLang == "tr-TR")
            currLangTx = "Türkçe";
    }
    private void ChangeLang(string value)
    {
        LocalStorage.SetItemAsString("Culture", value);
        NavigationManager.NavigateTo(NavigationManager.Uri, true);
    }
}