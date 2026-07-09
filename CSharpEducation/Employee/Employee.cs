namespace Employee;
/// <summary>
/// Модель Сотрудник.
/// </summary>
public abstract class Employee
{
  #region Поля и свойства
  
  /// <summary>
  /// Id сотрудника.
  /// </summary>
  public abstract int Id { get; init; }
  
  /// <summary>
  /// Имя сотрудника.
  /// </summary>
  public abstract string Name { get; set; }
  
  /// <summary>
  /// Базовая зарплата
  /// </summary>
  public abstract decimal BaseSalary { get; set; }

  #endregion

  #region Методы

  public abstract decimal CalculateSalary();

  /// <summary>
  /// Отображение сотрудника для консольного приложения
  /// </summary>
  /// <returns>Строковое значение сотрудника</returns>
  public override string ToString()
  {
    return $"ID: {Id}\n" +
           $"Имя: {Name}\n";
  }

  #endregion

  #region Конструкторы

  public Employee(int id, string name,  decimal salary)
  {
    Id = id;
    Name = name;
    BaseSalary = salary;
  }

  #endregion
}