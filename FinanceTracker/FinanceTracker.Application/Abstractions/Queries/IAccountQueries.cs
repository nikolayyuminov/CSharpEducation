using FinanceTracker.Application.Accounts.Queries.GetAccounts;

namespace FinanceTracker.Application.Abstractions.Queries;

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