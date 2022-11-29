using Dating.Shared.Models.Profile;

public sealed record GetProfileResult
{
    public bool IsCreated => Profile is not null;
    public ProfileVm? Profile { get; init; }
}