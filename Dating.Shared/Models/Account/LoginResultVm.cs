namespace Dating.Shared.Models.Account;

public sealed record LoginResultVm
{
    public Guid UserId { get; init; }
}