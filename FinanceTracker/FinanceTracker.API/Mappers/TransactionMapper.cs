using FinanceTracker.API.Contracts.Transactions;
using FinanceTracker.Application.Transactions.Commands;

namespace FinanceTracker.API.Mappers;

/// <summary>
/// Маппер для преобразования моделей транзакций между API и слоем Application.
/// </summary>
public class TransactionMapper
{
  /// <summary>
  /// Преобразовать HTTP-запрос на создание транзакции
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда создания транзакции.</returns>
  public static CreateTransactionCommand ToCreateTransactionCommand(CreateTransactionRequest request)
  {
    return new CreateTransactionCommand()
    {
      Description =  request.Description,
      AccountId =  request.AccountId,
      Amount =  request.Amount,
      CategoryId =  request.CategoryId,
      Kind =  request.Kind
    };
  }
  
  /// <summary>
  /// Преобразовать HTTP-запрос на изменение описания транзакции
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда изменения описания транзакции.</returns>
  public static ChangeTransactionDescriptionCommand ToChangeTransactionDescriptionCommand(ChangeTransactionDescriptionRequest request)
  {
    return new ChangeTransactionDescriptionCommand()
    {
      TransactionId =  request.TransactionId,
      NewDescription =  request.NewDescription
    };
  }
}