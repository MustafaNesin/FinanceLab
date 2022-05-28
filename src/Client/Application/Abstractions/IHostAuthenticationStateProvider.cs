using FinanceLab.Shared.Domain.Models.Dtos;

namespace FinanceLab.Client.Application.Abstractions;

public interface IHostAuthenticationStateProvider
{
    void SetAuthenticationState(UserDto? user);
}