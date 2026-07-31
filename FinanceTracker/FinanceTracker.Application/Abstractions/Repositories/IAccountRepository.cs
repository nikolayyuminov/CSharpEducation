using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория счетов.
/// </summary>
public interface IAccountRepository
{
  /// <summary>
  /// Добавление нового счета в репозиторий.
  /// </summary>
  /// <param name="account">Счет.</param>
  public void Add(Account account);
  
  /// <summary>
  /// Получение счета по Id.
  /// </summary>
  /// <param name="accountId">Id счета.</param>
  /// <returns>Найденный счет или null, если счет не существует.</returns>
  Account? GetById(long accountId);

  /// <summary>
  /// Получение счета по имени.
  /// </summary>
  /// <param name="userId">Id пользователя, которому принадлежит счет.</param>
  /// <param name="name">Имя счета для поиска.</param>
  /// <returns>Найденный счет или null, если счет не существует.</returns>
  Account? GetByName(long userId, string name);
}