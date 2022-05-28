using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Server.Domain.Models.Queries;

[PublicAPI]
public abstract class BaseListQuery
{
    protected BaseListQuery(string? filter, PaginationDto page, SortingDto sort)
        => (Filter, Page, Sort) = (filter, page, sort);

    public string? Filter { get; }
    public PaginationDto Page { get; }
    public SortingDto Sort { get; }
}