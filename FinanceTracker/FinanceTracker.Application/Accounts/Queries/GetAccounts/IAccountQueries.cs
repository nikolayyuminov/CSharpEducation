namespace FinanceTracker.Application.Accounts.Queries.GetAccounts;

/// <summary>
/// Запросы для чтения счетов.
/// </summary>
public interface IAccountQueries
{
  /// <summary>
  /// Получить список счетов пользователя.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  IReadOnlyCollection<AccountListItemDto> GetAll(long userId);
}