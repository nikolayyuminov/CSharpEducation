using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Abstractions.Services;

public interface ITransferService
{
  /// <summary>
  /// Перевод средств между счетами.
  /// </summary>
  /// <param name="command">Команда для перевода средств между счетами.</param>
  /// <returns>Ошибки при переводе. Если ошибок нет, перевод средств осуществился успешно.</returns>
  ValidationResult Transfer(TransferMoneyCommand command);
}