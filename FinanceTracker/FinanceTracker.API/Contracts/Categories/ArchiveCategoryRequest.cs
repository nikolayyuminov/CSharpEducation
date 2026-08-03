namespace FinanceTracker.API.Contracts.Categories;

/// <summary>
/// Запрос от пользователя для архивирования категории.
/// </summary>
public class ArchiveCategoryRequest
{
  /// <summary>
  /// Id категории, которую необходимо архивировать.
  /// </summary>
  public long CategoryId { get; init; }
}