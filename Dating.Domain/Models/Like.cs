using Dating.Domain.Enums;

namespace Dating.Domain.Models;

public sealed record Like : EntityBase
{
    /// <summary>
    /// Пользователь, поставивший лайк
    /// </summary>
    public required Guid UserFk { get; init; }
    
    /// <summary>
    /// Пользователь, которому поставили лайк
    /// </summary>
    public required Guid PairFk { get; init; }
    
    /// <summary>
    /// Статус лайка
    /// </summary>
    public required PairStatusEnum PairStatus { get; init; }
}
