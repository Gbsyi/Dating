using Dating.Api.CqrsUtils;
using Dating.Domain.Enums;
using Dating.Domain.Models;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.HttpErrors;
using Dating.Shared.Models.Pair;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Pair.Commands;

public sealed record DislikePairCommand(LikePairVm Vm) : IHttpRequest;

public sealed class DislikePairCommandHandler : IHttpRequestHandler<DislikePairCommand>
{
    private readonly IDatingDbContext _context;
    private readonly IUserContext _userContext;

    public DislikePairCommandHandler(IDatingDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<IResult> Handle(DislikePairCommand request, CancellationToken cancellationToken)
    {
        var vm = request.Vm;
        var userId = _userContext.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }

        var profile = await _context.Profiles.Where(x => x.Id == userId).FirstOrDefaultAsync(cancellationToken);
        if (profile is null)
        {
            return Results.Unauthorized();
        }

        var dislikedProfile = await _context.Profiles.Where(x => x.Id == vm.LikedProfileId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (dislikedProfile is null)
        {
            return Results.BadRequest(new BadRequestVm
            {
                ErrorMessage = "Профиль не найден"
            });
        }
        
        var dbLike = await _context.Likes.Where(x => x.PairFk == dislikedProfile.Id || x.UserFk == dislikedProfile.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (dbLike is null)
        {
            var like = new Like
            {
                UserFk = profile.Id,
                PairFk = dislikedProfile.Id,
                PairStatus = PairStatusEnum.Rejected
            };
            
            _context.Likes.Add(like);
        }
        else
        {
            dbLike.PairStatus = PairStatusEnum.Rejected;
        }

        await _context.SaveChangesAsync(cancellationToken);
        
        return Results.Ok();
    }
}