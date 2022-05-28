using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public record MarketDto(
    string BaseCoinCode,
    string QuoteCoinCode)
{
    [JsonIgnore] public string Symbol = BaseCoinCode + QuoteCoinCode;
}