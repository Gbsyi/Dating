namespace Dating.Domain.Models;

public sealed record Gender : EntityBase
{
    /// <summary>
    /// Название гендера
    /// </summary>
    public string Name { get; init; }
}
