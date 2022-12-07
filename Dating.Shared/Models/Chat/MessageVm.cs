namespace Dating.Shared.Models.Chat;

public sealed record MessageVm
{
    public required Guid MessageId { get; init; }
    public required string Text { get; init; }
    public required Guid UserId { get; set; }
    public required DateTime MessageDate { get; set; }
}
