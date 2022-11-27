namespace Dating.Domain.Models;

/// <summary>
/// Пара. Создаётся, в случае взаимной симпатии
/// </summary>
public sealed record Pair : EntityBase
{
    /// <summary>
    /// Пользователь в паре
    /// </summary>
    public required Guid UserFk { get; init; }
    
    /// <summary>
    /// Пользователь, с которым создалась пара
    /// </summary>
    public required Guid MatchedUserFk { get; init; }
    
    /// <summary>
    /// Чат в паре
    /// </summary>
    public required Guid ChatFk { get; init; }
}
