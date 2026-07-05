namespace Employee;
/// <summary>
/// Управление консолью.
/// </summary>
public static class ConsoleManager
{
  /// <summary>
  /// Консольные запросы для добавления нового сотрудника.
  /// </summary>
  public static void AddEmployee()
  {
    Console.Write("Имя: ");
    var name = Console.ReadLine() ?? throw new InvalidOperationException("Имя не может быть пустым");
    if (name.Trim() == string.Empty) throw new InvalidOperationException("Имя не может быть пустым");

    Console.Write("Должность: ");
    var position = Console.ReadLine() ?? throw new InvalidOperationException("Должность не может быть пустой");
    if (position.Trim() == string.Empty) throw new InvalidOperationException("Должность не может быть пустой");

    Console.Write("Почасовая ставка: ");
    var hourRate = decimal.Parse(Console.ReadLine() ?? 
                                 throw new InvalidOperationException("Часовая ставка не может быть пустой"));


    Console.Write("Количество часов: ");
    var hoursWorked = 
      int.Parse(Console.ReadLine() ?? 
          throw new InvalidOperationException("Количество отработанных часов не может быть пустым"));
    
    EmployeeManager.AddNewEmployee(name, position, hourRate, hoursWorked);

    Console.WriteLine("Сотрудник успешно добавлен.");
    Pause();
  }

  /// <summary>
  /// Вывести список всех сотрудников на экран.
  /// </summary>
  public static void ShowEmployees()
  {
    if (EmployeeManager.Employees == null)
      throw new NullReferenceException("Список сотрудников пуст.");

    if (EmployeeManager.Employees.Count == 0)
      throw new NullReferenceException("Количество сотрудников '0'");
    
    foreach (var emp in EmployeeManager.Employees)
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
    
    var emp = EmployeeManager.FindEmployeeByName(name);

    if (emp == null) throw new NullReferenceException("Сотрудник не найден.");
    Console.WriteLine(emp);
    Pause();
    return emp;
  }
  
  /// <summary>
  /// Консольные запросы для изменения данных сотрудника.
  /// </summary>
  public static void UpdateEmployee()
  {
    var emp = FindEmployee();
    if (emp == null) throw new NullReferenceException("Сотрудник не найден");

    Console.Write("Новое имя: ");
    var newName = Console.ReadLine() ?? throw new InvalidOperationException("Имя не может быть пустым");
    if (newName == string.Empty) throw new InvalidOperationException("Имя не может быть пустым");
    
    Console.Write("Новая должность: ");
    var newPosition = Console.ReadLine() ?? throw new InvalidOperationException("Должность не может быть пустой");
    if (newPosition == string.Empty) throw new InvalidOperationException("Должность не может быть пустой");
    
    Console.Write("Новая часовая ставка: ");
    var newHourRate = decimal.Parse(Console.ReadLine() ?? 
                                    throw new InvalidOperationException("Часовая ставка не может быть пустой"));
    
    Console.Write("Новое количество отработанных часов: ");
    var newHoursWorked = 
      int.Parse(Console.ReadLine() ?? 
          throw new InvalidOperationException("Количество отработанных часов не может быть пустым"));
    
    EmployeeManager.UpdateEmployee(emp, newName, newPosition, newHourRate, newHoursWorked);
    
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
    
    var salary = EmployeeManager.CalculateSalary(emp);

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



}