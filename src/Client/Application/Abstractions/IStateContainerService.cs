namespace FinanceLab.Client.Application.Abstractions;

public interface IStateContainerService
{
    event Action? StateHasChanged;
}