using Dating.Domain.Enums;

namespace Dating.Domain.Models;

public sealed record Like : EntityBase
{
    /// <summary>
    /// Пользователь, поставивший лайк
    /// </summary>
    public required Guid UserFk { get; set; }
    
    /// <summary>
    /// Пользователь, которому поставили лайк
    /// </summary>
    public required Guid PairFk { get; set; }
    
    /// <summary>
    /// Статус лайка
    /// </summary>
    public required PairStatusEnum PairStatus { get; set; }
}
