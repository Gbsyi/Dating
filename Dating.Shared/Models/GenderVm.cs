namespace Dating.Shared.Models;

public sealed record GenderVm
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
