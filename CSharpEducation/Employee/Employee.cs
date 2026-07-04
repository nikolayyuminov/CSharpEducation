namespace Employee;
/// <summary>
/// Модель Сотрудник.
/// </summary>
public class Employee
{
  #region Поля и свойства

  private static int CountId {get; set;}
  /// <summary>
  /// Id сотрудника.
  /// </summary>
  public int Id { get; set; }
  /// <summary>
  /// Имя сотрудника.
  /// </summary>
  public string Name { get; set; }
  /// <summary>
  /// Должность сотрудника.
  /// </summary>
  public string Position { get; set; }
  /// <summary>
  /// Часовая ставка сотрудника.
  /// </summary>
  public decimal HourRate { get; set; }
  /// <summary>
  /// Количество отработанных часов
  /// </summary>
  public int HoursWorked { get; set; }

  #endregion

  #region Методы

  /// <summary>
  /// Отображение сотрудника для консольного приложения
  /// </summary>
  /// <returns>Строковое значение сотрудника</returns>
  public override string ToString()
  {
    return $"ID: {Id}\n" +
           $"Имя: {Name}\n" +
           $"Должность: {Position}\n" +
           $"Ставка: {HourRate} руб./час\n" +
           $"Отработано часов: {HoursWorked}\n";
  }

  #endregion

  #region Конструкторы
/// <summary>
/// Конструктор.
/// </summary>
/// <param name="name">Имя сотрудника.</param>
/// <param name="position">Должность сотрудника.</param>
/// <param name="hourRate">Часовая ставка сотрудника.</param>
/// <param name="hoursWorked">Количество отработанных часов.</param>
  public Employee(string name, string position, decimal hourRate, int hoursWorked)
  {
    Id = CountId++;
    Name = name;
    Position = position;
    HourRate = hourRate;
    HoursWorked = hoursWorked;
  }

  #endregion



}