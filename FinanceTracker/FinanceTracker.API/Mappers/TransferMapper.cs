using FinanceTracker.API.Contracts.Transfers;
using FinanceTracker.Application.Transfers.Commands;

namespace FinanceTracker.API.Mappers;

/// <summary>
/// Маппер для преобразования моделей переводов между API и слоем Application.
/// </summary>
public static class TransferMapper
{
  /// <summary>
  /// Преобразовать HTTP-запрос на перевод средств между счетами
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда перевода средств между счетами.</returns>
  public static TransferMoneyCommand ToTransferMoneyCommand(TransferMoneyRequest request)
  {
    return new TransferMoneyCommand()
    {
      Amount =  request.Amount,
      Description =  request.Description,
      FromAccountId =  request.FromAccountId,
      ToAccountId =  request.ToAccountId
    };
  }
}