using Dating.Api.CqrsUtils;
using Dating.Application.Exceptions;
using Dating.Domain.Enums;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.Enums;
using Dating.Shared.Models;
using Dating.Shared.Models.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Profile.Queries;

public sealed record GetProfileQuery() : IHttpRequest;


internal sealed class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, IResult>
{
    private readonly IUserContext _userContext;
    private readonly IDatingDbContext _context;

    public GetProfileQueryHandler(IUserContext userContext, IDatingDbContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<IResult> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }

        var profile = await (from p in _context.Profiles
                                         join g in _context.Genders on p.GenderFk equals g.Id
                                         where p.Id == userId
                                         select new ProfileVm
                                         {
                                             Age = p.Age,
                                             Description = p.Description,
                                             Name = p.Name,
                                             Sex = new GenderVm
                                             {
                                                 Id = g.Id,
                                                 Name = g.Name
                                             }
                                         }).FirstOrDefaultAsync(cancellationToken);
        
        return Results.Ok(new GetProfileResult
        {
            Profile = profile
        });
    }
}