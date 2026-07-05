namespace Employee;

internal static class Program
{
  private static void Main()
  {
    try
    {
      EmployeeManager.LoadFromFile();
      ConsoleManager.Pause();
    }
    catch (FileNotFoundException message)
    {
      Console.WriteLine(message.Message);
      ConsoleManager.Pause();
    }
    catch (FileLoadException ex)
    {
      Console.WriteLine($"Ошибка при загрузке файла: {ex.Message}");
      ConsoleManager.Pause();
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      ConsoleManager.Pause();
    }
    
    while (true)
    {
      try
      {
        Console.Clear();
        Console.WriteLine("=== Управление сотрудниками ===");
        Console.WriteLine("1. Добавить сотрудника");
        Console.WriteLine("2. Показать всех сотрудников");
        Console.WriteLine("3. Найти сотрудника");
        Console.WriteLine("4. Обновить сотрудника");
        Console.WriteLine("5. Рассчитать зарплату");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите действие: ");

        switch (Console.ReadLine())
        {
          case "1":
            ConsoleManager.AddEmployee();
            break;
          case "2":
            ConsoleManager.ShowEmployees();
            break;
          case "3":
            ConsoleManager.FindEmployee();
            break;
          case "4":
            ConsoleManager.UpdateEmployee();
            break;
          case "5":
            ConsoleManager.CalculateSalary();
            break;
          case "0":
            return;
          default:
            Console.WriteLine("Неверный выбор.");
            ConsoleManager.Pause();
            break;
        }
      }
      catch (NullReferenceException ex)
      {
        Console.WriteLine(ex.Message);
        ConsoleManager.Pause();
      }
      
      catch (InvalidOperationException ex)
      {
        Console.WriteLine(ex.Message);
        ConsoleManager.Pause();
      }
      
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        ConsoleManager.Pause();
      }
    }
  }
}
