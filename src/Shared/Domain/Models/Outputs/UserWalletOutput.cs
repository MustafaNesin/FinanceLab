using FinanceLab.Shared.Domain.Models.Dtos;

namespace FinanceLab.Shared.Domain.Models.Outputs;

public record UserWalletOutput(IReadOnlyCollection<WalletDto> Wallet);