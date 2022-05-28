using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public sealed class PaginationDto
{
    public int Current { get; set; }
    public int Size { get; set; } = 10;
}