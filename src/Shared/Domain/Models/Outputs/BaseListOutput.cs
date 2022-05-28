namespace FinanceLab.Shared.Domain.Models.Outputs;

public abstract class BaseListOutput<TDto>
{
    protected BaseListOutput(IReadOnlyCollection<TDto> items, int total) => (Items, Total) = (items, total);

    public IReadOnlyCollection<TDto> Items { get; }
    public int Total { get; }
}