namespace Dating.Domain.Models;

public sealed record UserOrientation : EntityBase
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserFk { get; init; }
    
    /// <summary>
    /// Id гендера
    /// </summary>
    public required Guid GenderFk { get; init; }
}