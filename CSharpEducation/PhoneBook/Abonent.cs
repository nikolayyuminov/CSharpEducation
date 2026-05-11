namespace PhoneBook;

public class Abonent
{
  #region Свойства

  /// <summary>
  /// Номер телефона абонента.
  /// </summary>
  public string PhoneNumber { get; set; }
  
  /// <summary>
  /// Имя абонента.
  /// </summary>
  public string Name { get; set; }

  #endregion

  #region Методы

  /// <summary>
  /// Отображение информации об объекте.
  /// </summary>
  /// <returns>Строка с именем и телефоном абонента.</returns>
  public override string ToString()
  {
    return $"{Name}: {PhoneNumber}";
  }

  #endregion

  #region Конструктор

  /// <summary>
  /// Конструктор для создания абонента.
  /// </summary>
  /// <param name="name">Имя абонента.</param>
  /// <param name="phoneNumber">Номер телефона абонента.</param>
  public Abonent(string name, string phoneNumber)
  {
    PhoneNumber = phoneNumber;
    Name = name;
  }

  #endregion



}