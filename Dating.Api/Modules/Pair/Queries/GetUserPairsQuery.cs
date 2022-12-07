using Dating.Api.CqrsUtils;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.Models.Pair;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Pair.Queries;

public sealed record GetUserPairsQuery() : IHttpRequest;
public sealed class GetUserPairsQueryHandler : IHttpRequestHandler<GetUserPairsQuery>
{
    private readonly IDatingDbContext _context;
    private readonly IUserContext _userContext;

    public GetUserPairsQueryHandler(IDatingDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<IResult> Handle(GetUserPairsQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }

        var pairs = await (from p in _context.Pairs
                join matchedProfile in _context.Profiles on p.MatchedUserFk equals matchedProfile.Id
                where p.UserFk == userId
                select new PairVm
                {
                    UserId = matchedProfile.Id,
                    Name = matchedProfile.Name,
                    ChatId = p.ChatFk,
                    PictureId = matchedProfile.PictureFk
                }).ToListAsync(cancellationToken);

        return Results.Ok(pairs);
    }
}