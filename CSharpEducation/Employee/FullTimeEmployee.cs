namespace Employee;
/// <summary>
/// Полный сотрудник.
/// </summary>
public class FullTimeEmployee: Employee
{
  #region Поля и свойства
  
  /// <summary>
  /// Id сотрудника.
  /// </summary>
  public override int Id { get; init; }
  
  /// <summary>
  /// Имя сотрудника.
  /// </summary>
  public override string Name { get; set; }
  
  /// <summary>
  /// Базовая зарплата
  /// </summary>
  public override decimal BaseSalary { get; set; }

  #endregion
  
  #region Методы
  /// <summary>
  /// Расчет зарплаты.
  /// </summary>
  /// <returns>Зарплата сотрудника.</returns>
  public override decimal CalculateSalary()
  {
    return BaseSalary;
  }
  
  #endregion
  
  #region Конструкторы
  /// <summary>
  /// Конструктор полного сотрудника.
  /// </summary>
  /// <param name="id">Id нового сотрудника.</param>
  /// <param name="name">Имя.</param>
  /// <param name="salary">Зарплата.</param>
  public FullTimeEmployee(int id, string name, decimal salary) : base(id, name, salary)
  {
  }
  
  #endregion
}