using Dating.Api.CqrsUtils;
using Dating.Api.Modules.Gender.Queries;

namespace Dating.Api.Modules.Gender;

public static class GendersApiConfig
{
    public static void AddGendersApiConfig(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("gender").WithTags("Gender");

        group.MediateGet<GetGendersListQuery>("/list");
    }
}