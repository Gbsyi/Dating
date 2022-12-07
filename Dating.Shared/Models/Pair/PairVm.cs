namespace Dating.Shared.Models.Pair;

public sealed record PairVm
{
    public required Guid UserId { get; init; }
    public required string Name { get; init; }
    public required Guid? PictureId { get; init; }
    public Guid ChatId { get; init; }
}
