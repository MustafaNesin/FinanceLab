namespace FinanceLab.Client.Application.Abstractions;

public interface IHostAuthenticationStateProvider
{
    void RenewAuthenticationState();
}