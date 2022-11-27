namespace Dating.Domain.Models;

public sealed record Message : EntityBase
{
    /// <summary>
    /// Дата отправки сообщения
    /// </summary>
    public DateTime MessageDate { get; init; }
    
    /// <summary>
    /// Id отправителя
    /// </summary>
    public Guid UserFk { get; init; }
    
    /// <summary>
    /// Id чата
    /// </summary>
    public Guid ChatFk { get; init; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public required string Text { get; init; }
}