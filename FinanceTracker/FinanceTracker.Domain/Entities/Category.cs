using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Категории транзакций.
/// </summary>
public class Category
{
  #region Поля и свойства

  /// <summary>
  /// Уникальный идентификатор.
  /// </summary>
  public long Id { get; init; }
  
  /// <summary>
  /// Имя категории.
  /// </summary>
  public string Name { get; private set; }
  
  /// <summary>
  /// Id пользователя, которому принадлежит категория.
  /// Если значение null, значит категория системная.
  /// </summary>
  public long? UserId { get; init; }
  
  /// <summary>
  /// Состояние категории (Активна/Архивная)
  /// </summary>
  public bool IsArchived  { get; private set; } 
  
  /// <summary>
  /// Вид категории (Входящий/Исходящий).
  /// </summary>
  public CategoryKind CategoryKind { get; init; }
  
  /// <summary>
  /// Описание категории.
  /// </summary>
  public string? Description { get; private set; }

  #endregion

  #region Методы

  /// <summary>
  /// Изменить имя категории.
  /// </summary>
  /// <param name="newName">Новое имя категории.</param>
  /// <exception cref="InvalidOperationException">Имя категории не может быть пустым.</exception>
  public void Rename(string newName)
  {
    ValidateName(newName);
    EnsureCategoryIsNotSystem();
    EnsureCategoryIsActive();
    
    if (newName.Equals(Name)) return;
    
    Name = newName;
  }

  /// <summary>
  /// Изменить описание категории.
  /// </summary>
  /// <param name="newDescription">Значение нового описания.</param>
  public void ChangeDescription(string? newDescription)
  {
    EnsureCategoryIsNotSystem();
    EnsureCategoryIsActive();
    
    if (string.Equals(Description, newDescription)) return;
    
    Description = newDescription;
  }
  
  // TODO: Проверить, используется ли категория в незавершенных операциях
  // перед архивацией.
  /// <summary>
  /// Архивировать категорию.
  /// </summary>
  public void Archive()
  {
    EnsureCategoryIsNotSystem();
    EnsureCategoryIsActive();
    
    IsArchived = true;
  }

  /// <summary>
  /// Архивна ли категория.
  /// </summary>
  /// <exception cref="InvalidOperationException">Нельзя изменять архивную категорию</exception>
  private void EnsureCategoryIsActive()
  {
    if (IsArchived) throw new InvalidOperationException("Нельзя изменять архивную категорию");
  }
  
  /// <summary>
  /// Системная ли категория.
  /// </summary>
  /// <exception cref="InvalidOperationException">Нельзя изменять системную категорию</exception>
  private void EnsureCategoryIsNotSystem()
  {
    if (UserId == null) throw new InvalidOperationException("Нельзя изменять системную категорию");
  }

  /// <summary>
  /// Проверить корректность имени категории.
  /// </summary>
  /// <param name="name">Имя категории.</param>
  /// <exception cref="InvalidOperationException">Имя категории не может быть пустым.</exception>
  private static void ValidateName(string name)
  {
    if (string.IsNullOrWhiteSpace(name)) throw new InvalidOperationException("Имя категории не может быть пустым.");
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="name">Имя категории.</param>
  /// <param name="description">Описание категории.</param>
  /// <param name="userId">Id пользователя, которому принадлежит категория.</param>
  /// <param name="categoryKind">Вид категории (входящая/исходящая).</param>
  public Category(string name, string? description, long? userId, CategoryKind categoryKind)
  {
    ValidateName(name);
    Name = name;
    Description = description;
    UserId = userId;
    CategoryKind = categoryKind;
    IsArchived = false;
  }

  #endregion
}