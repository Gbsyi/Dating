namespace Dating.Shared.HttpErrors;

public sealed record BadRequestVm
{
    public required string ErrorMessage { get; init; }
}