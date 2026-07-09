namespace Employee;

internal static class Program
{
  private static void Main()
  {
    while (true)
    {
      try
      {
        Console.Clear();
        Console.WriteLine("=== Управление сотрудниками ===");
        Console.WriteLine("1. Добавить полного сотрудника");
        Console.WriteLine("2. Добавить частичного сотрудника");
        Console.WriteLine("3. Показать всех сотрудников");
        Console.WriteLine("4. Найти сотрудника");
        Console.WriteLine("5. Обновить сотрудника");
        Console.WriteLine("6. Удалить сотрудника");
        Console.WriteLine("7. Рассчитать зарплату");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите действие: ");

        switch (Console.ReadLine())
        {
          case "1":
            ConsoleManager.AddFullTimeEmployee();
            break;
          case "2":
            ConsoleManager.AddPartTimeEmployee();
            break;
          case "3":
            ConsoleManager.ShowEmployees();
            break;
          case "4":
            ConsoleManager.FindEmployee();
            break;
          case "5":
            ConsoleManager.UpdateEmployee();
            break;
          case "6":
            ConsoleManager.DeleteEmployee();
            break;
          case "7":
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
