namespace FinanceTracker.API.Contracts.Categories;

/// <summary>
/// Запрос от пользователя на переименование категории.
/// </summary>
public sealed record RenameCategoryRequest
{
  /// <summary>
  /// Id пользователя, которому принадлежит категория.
  /// </summary>
  public long UserId { get; init; }
  
  /// <summary>
  /// Id категории для переименования.
  /// </summary>
  public long CategoryId { get; init; }
  
  /// <summary>
  /// Новое имя для категории.
  /// </summary>
  public string? NewName { get; init; }
}