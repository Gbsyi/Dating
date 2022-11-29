using Dating.Api.Modules.Account;
using Dating.Api.Modules.Gender;
using Dating.Api.Modules.Profile;

namespace Dating.Api;

public static class MinimalApiConfiguration
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        app.AddAccountModuleEndpoints();
        app.AddProfileEndpoints();
        app.AddGendersApiConfig();
    }
}