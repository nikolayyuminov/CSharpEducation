using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Application.Transactions.Commands;

namespace FinanceTracker.Application.Abstractions.Services;

/// <summary>
/// Интерфейс сервиса для работы с транзакциями.
/// </summary>
public interface ITransactionService
{
  /// <summary>
  /// Создать транзакцию.
  /// </summary>
  /// <param name="command">Команда пользователя на создание транзакции.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  ValidationResult CreateTransaction(CreateTransactionCommand command);

  /// <summary>
  /// Изменение описания транзакции.
  /// </summary>
  /// <param name="command">Команда пользователя на изменение описания транзакции.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  ValidationResult ChangeDescription(ChangeTransactionDescriptionCommand command);
}