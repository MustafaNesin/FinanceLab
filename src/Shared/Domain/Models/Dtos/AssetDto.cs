using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public record AssetDto(string CoinCode, double Amount);