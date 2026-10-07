using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Categories.Queries.GetCategories;

/// <summary>
/// Элемент списка категорий.
/// </summary>
public sealed record CategoryListItemDto
{
  /// <summary>
  /// Уникальный идентификатор категории.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// Название категории.
  /// </summary>
  public string Name { get; init; } = string.Empty;

  /// <summary>
  /// Идентификатор пользователя, которому принадлежит категория.
  /// <para>
  /// Если значение равно <c>null</c>, категория является системной.
  /// </para>
  /// </summary>
  public long? UserId { get; init; }

  /// <summary>
  /// Признак того, что категория находится в архиве.
  /// </summary>
  public bool IsArchived { get; init; }

  /// <summary>
  /// Вид категории: входящая или исходящая.
  /// </summary>
  public CategoryKind CategoryKind { get; init; }

  /// <summary>
  /// Описание категории.
  /// </summary>
  public string? Description { get; init; }
}