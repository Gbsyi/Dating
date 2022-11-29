using Dating.Shared.Enums;

namespace Dating.Shared.Models.Profile;

public sealed record ProfileVm
{
    public required string Name { get; init; }
    public required int Age { get; init; }
    public required string Description { get; init; }
    public required GenderVm Sex { get; init; }
}
