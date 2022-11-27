namespace Dating.Domain.Models;

public sealed record User : EntityBase
{
    /// <summary>
    /// Логин
    /// </summary>
    public required string Username { get; init; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; init; }
};
