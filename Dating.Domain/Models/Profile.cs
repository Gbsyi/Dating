using Dating.Domain.Enums;

namespace Dating.Domain.Models;

public sealed record Profile : EntityBase
{
    /// <summary>
    /// Отображаемое имя
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Возраст
    /// </summary>
    public required int Age { get; init; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; init; }
    
    /// <summary>
    /// Пол
    /// </summary>
    public required SexEnum Sex { get; init; }
}
