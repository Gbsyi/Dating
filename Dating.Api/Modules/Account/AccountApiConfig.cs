using Dating.Api.CqrsUtils;
using Dating.Api.Modules.Account.Commands;
using Dating.Shared.Models.Account;

namespace Dating.Api.Modules.Account;

internal static class AccountApiConfig
{
    public static void AddAccountModuleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("account").WithTags("Account");
        
        group.MediatePost<RegisterCommand, LoginResultVm>("/register");
        group.MediatePost<LoginCommand, LoginResultVm>("/login");
    }
}