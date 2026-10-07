using BlazorApp1.Models;

namespace BlazorApp1.Services;

/// <summary>
/// Клиент для работы с категориями.
/// </summary>
public class CategoryApiClient
{
  #region Поля

  /// <summary>
  /// Http клиент.
  /// </summary>
  private readonly HttpClient _httpClient;

  /// <summary>
  /// Текущий пользователь.
  /// </summary>
  private readonly CurrentUserService _currentUser;

  #endregion
  
  #region Методы

  /// <summary>
  /// Получить категории системные и текущего пользователя.
  /// </summary>
  public async Task<IReadOnlyCollection<CategoryModel>> GetCategoryAsync()
  {
    var result = await _httpClient.GetFromJsonAsync<List<CategoryModel>>($"api/categories");

    return result ?? [];
  }
  
  #endregion
  
  #region Конструкторы

  public CategoryApiClient(IHttpClientFactory factory, CurrentUserService currentUser)
  {
    _httpClient = factory.CreateClient("FinanceTrackerApi");
    _currentUser = currentUser;
  }

  #endregion
}