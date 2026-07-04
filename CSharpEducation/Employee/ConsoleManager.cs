namespace Employee;
/// <summary>
/// Управление консолью.
/// </summary>
public class ConsoleManager
{
  /// <summary>
  /// Консольные запросы для добавления нового сотрудника.
  /// </summary>
  public static void AddEmployee()
  {
    Console.Write("Имя: ");
    var name = Console.ReadLine() ?? string.Empty;
    Console.WriteLine();

    Console.Write("Должность: ");
    var position = Console.ReadLine() ?? string.Empty;
    Console.WriteLine();

    Console.Write("Почасовая ставка: ");
    var hourRate = decimal.Parse(Console.ReadLine() ?? "0");
    Console.WriteLine();

    Console.Write("Количество часов: ");
    var hoursWorked = int.Parse(Console.ReadLine() ?? "0");
    Console.WriteLine();
    
    EmployeeManager.AddNewEmployee(name, position, hourRate, hoursWorked);

    Console.WriteLine("Сотрудник успешно добавлен.");
    Pause();
  }

  /// <summary>
  /// Вывести список всех сотрудников на экран.
  /// </summary>
  public static void ShowEmployees()
  {
    if (EmployeeManager.Employees.Count == 0)
    {
      Console.WriteLine("Список сотрудников пуст.");
    }
    else
    {
      foreach (var emp in EmployeeManager.Employees)
      {
        Console.WriteLine(emp);
        Console.WriteLine(new string('-', 30));
      }
    }

    Pause();
  }
  
  /// <summary>
  /// Консольные запросы для поиска сотрудника по имени.
  /// </summary>
  /// <returns>Сотрудник.</returns>
  public static Employee? FindEmployee()
  {
    Console.Write("Введите имя сотрудника: ");
    var name = Console.ReadLine() ?? string.Empty;
    Console.WriteLine();
    
    var emp = EmployeeManager.FindEmployeeByName(name);

    if (emp != null)
    {
      Console.WriteLine(emp);
      Pause();
      return emp;
    }
    Console.WriteLine("Сотрудник не найден.");
    Pause();
    return null;
  }
  
  /// <summary>
  /// Консольные запросы для изменения данных сотрудника.
  /// </summary>
  public static void UpdateEmployee()
  {
    var emp = FindEmployee();

    Console.Write("Новое имя: ");
    var newName = Console.ReadLine() ?? string.Empty;
    
    Console.Write("Новая должность: ");
    var newPosition = Console.ReadLine() ?? string.Empty;
    
    Console.Write("Новая ставка: ");
    var newHourRate = decimal.Parse(Console.ReadLine() ?? "0");
    
    Console.Write("Новое количество часов: ");
    var newHoursWorked = int.Parse(Console.ReadLine() ?? "0");
    
    EmployeeManager.UpdateEmployee(emp, newName, newPosition, newHourRate, newHoursWorked);
    
    Console.WriteLine("Данные обновлены.");
    Pause();
  }
  
  public static void CalculateSalary()
  {
    var emp = FindEmployee();
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