namespace FinanceTracker.API.Contracts.Categories;

/// <summary>
/// Запрос от пользователя для архивирования категории.
/// </summary>
public sealed record ArchiveCategoryRequest
{
  /// <summary>
  /// Id категории, которую необходимо архивировать.
  /// </summary>
  public long CategoryId { get; init; }
}