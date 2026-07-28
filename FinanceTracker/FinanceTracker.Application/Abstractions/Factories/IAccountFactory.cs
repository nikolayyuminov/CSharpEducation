using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Abstractions.Factories;

/// <summary>
/// Интерфейс фабрики для создания счета.
/// </summary>
public interface IAccountFactory
{
  /// <summary>
  /// Создать счет.
  /// </summary>
  /// <param name="command">Команда с данными для создания счета.</param>
  /// <returns>Счет.</returns>
  public Account Create(CreateAccountCommand command);
}