using System.ComponentModel;

namespace FinanceLab.Shared.Domain.Models.Inputs;

public class UserListInput
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Search { get; set; }
    public string? Sort { get; set; }
    public ListSortDirection? SortDirection { get; set; }
}