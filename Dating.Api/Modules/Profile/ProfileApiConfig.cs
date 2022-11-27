namespace Dating.Api.Modules.Profile;

public static class ProfileApiConfig
{
    public static void AddProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("profile").WithTags("Profile");
        
        
    }
}