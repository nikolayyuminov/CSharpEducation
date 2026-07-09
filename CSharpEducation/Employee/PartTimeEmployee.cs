namespace Employee;

public class PartTimeEmployee : Employee
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
  /// Количество отработанных часов
  /// </summary>
  public int HoursWorked { get; set; }
  
  /// <summary>
  /// Базовая зарплата
  /// </summary>
  public override decimal BaseSalary { get; set; }

  #endregion
  
  #region Методы

  public override decimal CalculateSalary()
  {
    return BaseSalary *  HoursWorked;
  }
  
  #endregion
  
  #region Конструкторы

    public PartTimeEmployee(int id, string name, decimal salary, int hoursWorked) : base(id, name, salary)
    {
      HoursWorked = hoursWorked; 
    }
    

  #endregion

}