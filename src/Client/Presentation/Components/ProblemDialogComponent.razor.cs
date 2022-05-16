using FinanceLab.Client.Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class ProblemDialogComponent
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public ProblemDetails? ProblemDetails { get; set; }

    private void Close() => MudDialog.Close();
}