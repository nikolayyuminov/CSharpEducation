using BlazorApp1.Models;

namespace BlazorApp1.Services;

/// <summary>
/// Клиент для работы со счетами.
/// </summary>
public class AccountApiClient
{
  #region Поля

  /// <summary>
  /// Http клиент.
  /// </summary>
  private readonly HttpClient _httpClient;

  #endregion

  #region Методы

  /// <summary>
  /// Получить список счетов пользователя.
  /// </summary>
  public async Task<IReadOnlyCollection<AccountModel>> GetAccountsAsync(long userId)
  {
    var result = await _httpClient.GetFromJsonAsync<List<AccountModel>>
    (
      $"api/accounts?userId={userId}"
    );

    return result ?? [];
  }

  #endregion

  #region Конструкторы

  public AccountApiClient(IHttpClientFactory factory)
  {
    _httpClient = factory.CreateClient("FinanceTrackerApi");
  }

  #endregion
}