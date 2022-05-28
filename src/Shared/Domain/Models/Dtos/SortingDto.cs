using System.ComponentModel;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public sealed class SortingDto
{
    public string? By { get; set; }
    public ListSortDirection Direction { get; set; }
}