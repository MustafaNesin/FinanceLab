using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Components;

public partial class CultureSelectComponent
{
    private readonly CultureInfo[] _supportedCultures =
    {
        new("en-US"),
        new("tr-TR")
    };

    [Inject]
    private ISyncLocalStorageService LocalStorage { get; set; } = default!;

    private CultureInfo Culture
    {
        get => CultureInfo.CurrentCulture;
        set
        {
            if (value.Equals(CultureInfo.CurrentCulture))
                return;

            LocalStorage.SetItemAsString("Culture", value.Name);
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }
    }
}