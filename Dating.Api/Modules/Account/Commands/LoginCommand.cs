using Dating.Api.CqrsUtils;
using Dating.Infrastructure;
using Dating.Shared.HttpErrors;
using Dating.Shared.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Account.Commands;

public sealed record LoginCommand(LoginVm LoginVm) : IHttpRequest;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, IResult>
{
    private readonly IDatingDbContext _context;

    public LoginCommandHandler(IDatingDbContext context)
    {
        _context = context;
    }

    public async Task<IResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var vm = request.LoginVm;
        var user = await _context.Users.Where(x => x.Username == vm.Username).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            return Results.BadRequest(new BadRequestVm
            {
                ErrorMessage = "Неверное имя пользователя или пароль"
            });
        }

        if (user.Password != vm.Password)
        {
            return Results.BadRequest(new BadRequestVm
            {
                ErrorMessage = "Неверное имя пользователя или пароль"
            });
        }

        return Results.Ok(new LoginResultVm
        {
            UserId = user.Id
        });
    }
}
