using System.Net;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;
using Mapster;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Queries;

[UsedImplicitly]
public sealed class GetUserQueryHandler : BaseRequestHandler<GetUserQuery, UserDto>
{
    private readonly IMongoDbContext _dbContext;

    public GetUserQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources) : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Find(u => u.UserName == request.UserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            Throw(HttpStatusCode.NotFound, L["UserNotFound"]);

        return user.Adapt<UserDto>();
    }
}