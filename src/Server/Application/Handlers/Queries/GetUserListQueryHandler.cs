using System.ComponentModel;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;
using Mapster;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Queries;

[UsedImplicitly]
public sealed class GetUserListQueryHandler : BaseRequestHandler<GetUserListQuery, UserListOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetUserListQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources) : base(sharedResources)
        => _dbContext = dbContext;

    public override Task<UserListOutput> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users.AsQueryable().AsQueryable();

        if (request.Search is { Length: > 0 })
            query = from user in query
                where user.FirstName.Contains(request.Search) ||
                      user.LastName.Contains(request.Search) ||
                      user.UserName.Contains(request.Search)
                select user;

        query = request.Sort switch
        {
            nameof(UserDto.FirstName) => request.SortDirection switch
            {
                ListSortDirection.Ascending => from user in query orderby user.FirstName select user,
                ListSortDirection.Descending => from user in query orderby user.FirstName descending select user,
                _ => query
            },
            nameof(UserDto.LastName) => request.SortDirection switch
            {
                ListSortDirection.Ascending => from user in query orderby user.LastName select user,
                ListSortDirection.Descending => from user in query orderby user.LastName descending select user,
                _ => query
            },
            nameof(UserDto.UserName) => request.SortDirection switch
            {
                ListSortDirection.Ascending => from user in query orderby user.UserName select user,
                ListSortDirection.Descending => from user in query orderby user.UserName descending select user,
                _ => query
            },
            nameof(UserDto.Role) => request.SortDirection switch
            {
                ListSortDirection.Ascending => from user in query orderby user.Role select user,
                ListSortDirection.Descending => from user in query orderby user.Role descending select user,
                _ => query
            },
            nameof(UserDto.RegisteredAt) => request.SortDirection switch
            {
                ListSortDirection.Ascending => from user in query orderby user.RegisteredAt select user,
                ListSortDirection.Descending => from user in query orderby user.RegisteredAt descending select user,
                _ => query
            },
            _ => query
        };

        var totalItems = query.Count();

        query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);

        var items = query.ToList().Adapt<IReadOnlyCollection<UserDto>>();
        var output = new UserListOutput(items, totalItems);

        return Task.FromResult(output);
    }
}