using FinanceLab.Shared.Domain.Models.Dtos;

namespace FinanceLab.Shared.Domain.Models.Inputs;

public abstract class BaseListInput
{
    public string? Filter { get; set; }
    public PaginationDto Page { get; } = new();
    public SortingDto Sort { get; } = new();
}