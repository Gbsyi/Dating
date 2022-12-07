namespace Dating.Shared.Models.Chat;

public sealed record MessageSendVm
{
    public required Guid ChatId { get; init; }
    public required string Text { get; init; }
}
