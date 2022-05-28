using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public record WalletListOutput(
    IReadOnlyCollection<WalletDto> Wallets);