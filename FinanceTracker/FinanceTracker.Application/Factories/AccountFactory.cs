using FinanceTracker.Application.Abstractions.Factories;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Factories;

/// <summary>
/// Фабрика для создания счета.
/// </summary>
public class AccountFactory : IAccountFactory
{
  /// <summary>
  /// Создать счет.
  /// </summary>
  /// <param name="command">Команда с данными для создания счета.</param>
  /// <returns>Счет.</returns>
  public Account Create(CreateAccountCommand command)
  {
    var account = new Account(command.UserId, 
                              command.Name, 
                              command.AccountType, 
                              command.Currency, 
                              command.CreditLimit, 
                              command.InitialBalance);
    return account;
  }
}