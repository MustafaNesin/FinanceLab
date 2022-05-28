using System.ComponentModel;
using FinanceLab.Shared.Domain.Models.Outputs;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Queries;

public sealed record GetUserListQuery(int Page, int PageSize, string? Search, string? Sort,
    ListSortDirection? SortDirection) : IRequest<UserListOutput>;