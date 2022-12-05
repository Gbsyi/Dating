namespace Dating.Shared.Models.Pair;

public sealed record LikePairVm
{
    public Guid LikedProfileId { get; init; }
}