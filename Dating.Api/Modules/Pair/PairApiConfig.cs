using Dating.Api.CqrsUtils;
using Dating.Api.Modules.Pair.Commands;
using Dating.Api.Modules.Pair.Queries;
using Dating.Api.Modules.Profile.Queries;

namespace Dating.Api.Modules.Pair;

public static class PairApiConfig
{
    public static void AddPairEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("pair").WithTags("Pair");

        group.MediateGet<GetNextPairQuery>("/next");
        group.MediatePost<LikePairCommand>("/like");
        group.MediatePost<DislikePairCommand>("/dislike");
        group.MediateGet<GetUserPairsQuery>("/pairs");
    }
}