using Dating.Api.CqrsUtils;
using Dating.Domain.Enums;
using Dating.Domain.Models;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.Enums;
using Dating.Shared.HttpErrors;
using Dating.Shared.Models.Profile;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Profile.Commands;

internal sealed record CreateProfileCommand(CreateProfileVm CreateProfileVm) : IHttpRequest;

internal sealed class CreateProfileCommandHandler : IHttpRequestHandler<CreateProfileCommand>
{
    private readonly IDatingDbContext _context;
    private readonly IUserContext _userContext;
    
    public CreateProfileCommandHandler(IDatingDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<IResult> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var id = _userContext.UserId;
        if (id is null)
        {
            return Results.Unauthorized();
        }

        var user = await _context.Users.Where(x => x.Id == id.Value).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            return Results.Unauthorized();
        }

        var vm = request.CreateProfileVm;
        var profile = new Domain.Models.Profile
        {
            Id = user.Id,
            Name = vm.Name,
            Age = vm.Age,
            Description = vm.Description,
            GenderFk = vm.GenderId
        };
        var dbGenders = await _context.Genders.Where(x => vm.PreferredGenders.Contains(x.Id)).ToListAsync(cancellationToken);
        if (dbGenders.Count != vm.PreferredGenders.Count)
        {
            return Results.BadRequest(new BadRequestVm
            {
                ErrorMessage = "Присутствует несуществующий гендер"
            });
        }
        
        var genders = vm.PreferredGenders.Select(x => new UserOrientation
        {
            GenderFk = x,
            UserFk = user.Id
        });

        _context.Profiles.Add(profile);
        _context.UserOrientations.AddRange(genders);

        await _context.SaveChangesAsync(cancellationToken);

        return Results.Ok(new CreateProfileResultVm(profile.Id));
    }
}
