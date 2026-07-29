using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория счетов.
/// </summary>
public interface IAccountRepository
{
  /// <summary>
  /// Проверка существования имени счета в репозитории.
  /// </summary>
  /// <param name="userId">Id пользователя, которому принадлежит счет.</param>
  /// <param name="name">Имя счета для проверки уникальности.</param>
  /// <returns>True если имя уникально.</returns>
  public bool ExistsWithName(long userId, string name);

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
}