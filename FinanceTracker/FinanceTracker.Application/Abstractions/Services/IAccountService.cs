using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Abstractions.Services;

/// <summary>
/// Интерфейс сервиса для работы со счетом.
/// </summary>
public interface IAccountService
{
  /// TODO
  /// Написать тесты.
  /// <summary>
  /// Создание счета.
  /// </summary>
  /// <param name="command">Команда с данными для создания счета.</param>
  /// <returns>Ошибки при создании. Если ошибок нет, счет создался успешно.</returns>
  ValidationResult CreateAccount(CreateAccountCommand command);
  
  /// <summary>
  /// Переименовать счет.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование счета.</param>
  /// <returns>Ошибки при переименовании. Если ошибок нет, счет переименовался успешно.</returns>
  ValidationResult RenameAccount(RenameAccountCommand command);
  
  /// TODO
  /// Написать тесты.
  /// <summary>
  /// Закрыть счет.
  /// </summary>
  /// <param name="command">Команда от пользователя на закрытие счета.</param>
  /// <returns>Ошибки при закрытии. Если ошибок нет, счет закрыт.</returns>
  ValidationResult CloseAccount(CloseAccountCommand command);
}