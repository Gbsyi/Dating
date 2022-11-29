using Dating.Api.CqrsUtils;
using Dating.Infrastructure;
using Dating.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Gender.Queries;

public sealed record GetGendersListQuery() : IHttpRequest;

public sealed class GetGendersListQueryHandler : IRequestHandler<GetGendersListQuery, IResult>
{
    private readonly IDatingDbContext _context;

    public GetGendersListQueryHandler(IDatingDbContext context)
    {
        _context = context;
    }

    public async Task<IResult> Handle(GetGendersListQuery request, CancellationToken cancellationToken)
    {
        var genders = await _context.Genders.Select(x => new GenderVm
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync(cancellationToken);

        return Results.Ok(genders);
    }
}