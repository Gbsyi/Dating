using Dating.Shared.Enums;

namespace Dating.Shared.Models.Profile;

public sealed record CreateProfileVm
{
    public required string Name { get; init; }
    public required int Age { get; init; }
    public required string Description { get; init; }
    public required Guid GenderId { get; init; }
    public required IList<Guid> PreferredGenders { get; init; }
}
