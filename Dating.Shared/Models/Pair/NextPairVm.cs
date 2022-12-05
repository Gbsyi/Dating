namespace Dating.Shared.Models.Pair;

public sealed record NextPairVm
{
    public required Guid UserId { get; init; }
    public required int Age { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Guid PictureId { get; init; }
    public required string GenderName { get; init; }
}
