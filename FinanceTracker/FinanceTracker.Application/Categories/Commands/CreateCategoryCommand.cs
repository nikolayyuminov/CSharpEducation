using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Categories.Commands;

/// <summary>
/// Команда с данными для создания категории.
/// </summary>
public class CreateCategoryCommand
{
  /// <summary>
  /// Имя категории.
  /// </summary>
  public required string Name { get; init; }
  
  /// <summary>
  /// Id пользователя, которому принадлежит категория.
  /// Если значение null, значит категория системная.
  /// </summary>
  public long? UserId { get; init; }
  
  /// <summary>
  /// Вид категории (Входящий/Исходящий).
  /// </summary>
  public CategoryKind CategoryKind { get; init; }
  
  /// <summary>
  /// Описание категории.
  /// </summary>
  public string? Description { get; init; }
}