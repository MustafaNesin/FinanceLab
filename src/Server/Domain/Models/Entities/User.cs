using JetBrains.Annotations;

namespace FinanceLab.Server.Domain.Models.Entities;

[PublicAPI]
public sealed class User
{
    public string Id { get; init; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string? Role { get; set; }
    public DateTimeOffset RegisteredAt { get; init; } = DateTimeOffset.UtcNow;

    // public ICollection<Trade> Trades { get; init; } = new List<Trade>();
    // public ICollection<Transfer> Transfers { get; init; } = new List<Transfer>();
    public ICollection<Wallet> Wallets { get; init; } = new List<Wallet>();
}