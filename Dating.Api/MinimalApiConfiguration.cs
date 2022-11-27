using Dating.Api.Modules.Account;

namespace Dating.Api;

public static class MinimalApiConfiguration
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        app.AddAccountModuleEndpoints();
    }
}