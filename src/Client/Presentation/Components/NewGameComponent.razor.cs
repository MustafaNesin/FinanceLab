using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class NewGameComponent
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    private void Submit() => MudDialog.Close(DialogResult.Ok(true));
    private void Cancel() => MudDialog.Cancel();
}