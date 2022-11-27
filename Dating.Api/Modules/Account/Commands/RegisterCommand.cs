using Dating.Api.CqrsUtils;
using Dating.Domain.Models;
using Dating.Infrastructure;
using Dating.Shared.HttpErrors;
using Dating.Shared.Models.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Account.Commands;

public record RegisterCommand(RegisterVm RegisterVm) : IHttpRequest;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, IResult>
{
    private readonly IDatingDbContext _context;

    public RegisterCommandHandler(IDatingDbContext context)
    {
        _context = context;
    }

    public async Task<IResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var vm = request.RegisterVm;

        var dbUser = await _context.Users.Where(x => x.Username == vm.Username).FirstOrDefaultAsync(cancellationToken);

        if (dbUser is not null)
        {
           return Results.BadRequest( new BadRequestVm
           {
               ErrorMessage = "Пользователь с данным именем уже существует"
           });
        }

        var newUser = new User
        {
            Username = vm.Username,
            Password = vm.Password
        };

        _context.Users.Add(newUser);

        await _context.SaveChangesAsync(cancellationToken);

        return Results.Ok(new LoginResultVm
        {
            UserId = newUser.Id
        });
    }
}