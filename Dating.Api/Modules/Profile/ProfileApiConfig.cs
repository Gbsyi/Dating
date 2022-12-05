using Dating.Api.CqrsUtils;
using Dating.Api.Modules.Profile.Commands;
using Dating.Api.Modules.Profile.Queries;

namespace Dating.Api.Modules.Profile;

public static class ProfileApiConfig
{
    public static void AddProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("profile").WithTags("Profile");

        group.MediateGet<GetProfileQuery>("/");
        group.MediatePost<CreateProfileCommand>("/create");
    }
}