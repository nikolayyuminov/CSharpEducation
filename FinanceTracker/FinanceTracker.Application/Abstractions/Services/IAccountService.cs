using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Abstractions.Services;

/// <summary>
/// Интерфейс сервиса для работы со счетом.
/// </summary>
public interface IAccountService
{
  /// <summary>
  /// Создание счета.
  /// </summary>
  /// <param name="command">Команда с данными для создания счета.</param>
  /// <returns>Ошибки при создании. Если ошибок нет, счет создался успешно.</returns>
  ValidationResult CreateAccount(CreateAccountCommand command);
}