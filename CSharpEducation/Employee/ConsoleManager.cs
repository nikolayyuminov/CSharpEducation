namespace Employee;
/// <summary>
/// Управление консолью.
/// </summary>
public static class ConsoleManager
{
  /// <summary>
  /// Консольные запросы для добавления нового полного сотрудника.
  /// </summary>
  public static void AddFullTimeEmployee()
  {
    Console.Write("Имя: ");
    var name = Console.ReadLine();
    if (name == null || name.Trim().Equals(string.Empty)) throw new InvalidOperationException("Имя не может быть пустым");

    Console.Write("Зарплата: ");
    var salary = 
      decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Зарплата не может быть пустой"));
    
    var empMan = new EmployeeManager<FullTimeEmployee>();
    
    var newFullTimeEmp = empMan.CreateNewEmployee(name,  salary);
    empMan.Add(newFullTimeEmp);

    Console.WriteLine("Сотрудник успешно добавлен.");
    Pause();
  }
  
  /// <summary>
  /// Консольные запросы для добавления нового частичного сотрудника.
  /// </summary>
  public static void AddPartTimeEmployee()
  {
    Console.Write("Имя: ");
    var name = Console.ReadLine();
    if (name == null || name.Trim().Equals(string.Empty)) throw new InvalidOperationException("Имя не может быть пустым");

    Console.Write("Количество часов: ");
    var hoursWorked = 
      int.Parse(Console.ReadLine() ?? 
                throw new InvalidOperationException("Количество отработанных часов не может быть пустым"));
    
    Console.Write("Базовая зарплата: ");
    var salary = 
      decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Зарплата не может быть пустой"));
    
    var empMan = new EmployeeManager<PartTimeEmployee>();
    
    var newPartTimeEmp = empMan.CreateNewEmployee(name,  salary, hoursWorked);
    empMan.Add(newPartTimeEmp);

    Console.WriteLine("Сотрудник успешно добавлен.");
    Pause();
  }

  /// <summary>
  /// Вывести список всех сотрудников на экран.
  /// </summary>
  public static void ShowEmployees()
  {
 
    var empMan = new EmployeeManager<Employee>();
    var allEmployees = empMan.GetAll();
    
    foreach (var emp in allEmployees)
    {
      Console.WriteLine(emp);
      Console.WriteLine(new string('-', 30));
    }
    
    Pause();
  }
  
  /// <summary>
  /// Консольные запросы для поиска сотрудника по имени.
  /// </summary>
  /// <returns>Сотрудник.</returns>
  public static Employee FindEmployee()
  {
    Console.Write("Введите имя сотрудника: ");
    var name = Console.ReadLine() ?? throw new InvalidOperationException("Имя не может быть пустым");
    Console.WriteLine();
    
    var emps = new EmployeeManager<Employee>();
    var emp = emps.Get(name);

    if (emp == null) throw new NullReferenceException("Сотрудник не найден.");
    Console.WriteLine(emp);
    Pause();
    return emp;
  }
  
  /// <summary>
  /// Изменение данных сотрудника.
  /// </summary>
  public static void UpdateEmployee()
  {
    var emps = new EmployeeManager<Employee>();
    
    var emp = FindEmployee();
    if (emp == null) throw new NullReferenceException("Сотрудник не найден");
    emps.Update(emp);
    
    Console.WriteLine("Данные обновлены.");
    Pause();
  }
  
  /// <summary>
  /// Расчет зарплаты для сотрудника.
  /// </summary>
  public static void CalculateSalary()
  {
    var emp = FindEmployee();
    if (emp == null) throw new NullReferenceException("Сотрудник не найден");
    
    var salary = emp.CalculateSalary();

    Console.WriteLine($"Зарплата сотрудника {emp.Name}: {salary}");
    Pause();
  }
  
  /// <summary>
  /// Пауза для задержки информации на экране консоли.
  /// </summary>
  public static void Pause()
  {
    Console.WriteLine("\nНажмите любую клавишу...");
    Console.ReadKey();
  }
  
  /// <summary>
  /// Удаление сотрудника.
  /// </summary>
  public static void DeleteEmployee()
  {
    var empMan = new EmployeeManager<Employee>();
    var emp = FindEmployee();
    
    empMan.DeleteEmployee(emp);
  }
}