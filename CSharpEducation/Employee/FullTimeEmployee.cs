namespace Employee;

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

  public override decimal CalculateSalary()
  {
    return BaseSalary;
  }
  
  #endregion
  
  #region Конструкторы
  
  public FullTimeEmployee(int id, string name, decimal salary) : base(id, name, salary)
  {
  }
  
  #endregion
}