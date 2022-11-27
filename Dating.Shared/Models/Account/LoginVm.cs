namespace Dating.Shared.Models.Account;

public sealed record LoginVm
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}