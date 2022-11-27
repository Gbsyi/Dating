namespace Dating.Domain.Models;

public sealed record Chat : EntityBase
{
    /// <summary>
    /// Активный ли чат?
    /// </summary>
    public required bool IsActive { get; init; }
}
