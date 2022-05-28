using FinanceLab.Shared.Domain.Models.Dtos;

namespace FinanceLab.Client.Application.Abstractions;

public interface IStateContainerService
{
    UserDto? User { get; }
    event Action? StateHasChanged;

    void SetUser(UserDto? user, bool notifyStateHasChanged = true);
}