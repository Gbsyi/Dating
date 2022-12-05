using Dating.Api.CqrsUtils;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.Models.Pair;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Pair.Queries;

public sealed record GetNextPairQuery() : IHttpRequest;

public sealed record ProfileQueryModel;

public sealed class GetNextPairQueryHandler : IHttpRequestHandler<GetNextPairQuery>
{
    private readonly IUserContext _userContext;
    private readonly IDatingDbContext _context;
    
    public GetNextPairQueryHandler(IUserContext userContext, IDatingDbContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<IResult> Handle(GetNextPairQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }

        var genders = await (from p in _context.Profiles
            join o in _context.UserOrientations on p.Id equals o.UserFk
            where p.Id == userId
            select o.GenderFk).ToListAsync(cancellationToken);

        var nextProfile = await (from p in _context.Profiles
            join pic in _context.Pictures on p.PictureFk equals pic.Id
            join g in _context.Genders on p.GenderFk equals g.Id
            where p.Id != userId && genders.Contains(g.Id)
            select new NextPairVm
            {
                UserId = p.Id,
                Age = p.Age,
                Name = p.Name,
                Description = p.Description,
                PictureId = pic.Id,
                GenderName = g.Name
            }).FirstOrDefaultAsync(cancellationToken);

        return Results.Ok(nextProfile);
    }
}