using FinanceTracker.Application.Abstractions.Factories;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

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
    switch (command.AccountType)
    {
      case AccountType.Debit:
        return new DebitAccount(command.UserId,
                                      command.Name,
                                      command.AccountType,
                                      command.Currency,
                                      command.InitialBalance);
        
      case AccountType.Credit:
        return new CreditAccount(command.UserId,
                                    command.Name,
                                    command.Currency,
                                    command.CreditLimit,
                                    command.InitialBalance);
        
      case AccountType.Deposit:
        return new DepositAccount(command.UserId,
                                    command.Name,
                                    command.Currency,
                                    command.InitialBalance);
        
      default:
        throw new InvalidOperationException("Счет не создался, что-то пошло не так");
    }
  }
}