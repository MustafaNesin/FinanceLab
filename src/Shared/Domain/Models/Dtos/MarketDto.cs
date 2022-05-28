using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public sealed record MarketDto(
    string BaseCoinCode,
    string QuoteCoinCode)
{
    [JsonIgnore] public string Symbol = BaseCoinCode + QuoteCoinCode;
}