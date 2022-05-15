using FinanceLab.Client.Application.Abstractions;

namespace FinanceLab.Client.Application.Services;

public class StateContainerService : IStateContainerService
{
    public event Action? StateHasChanged;

    // ReSharper disable once UnusedMember.Local
    private void NotifyStateHasChanged() => StateHasChanged?.Invoke();
}