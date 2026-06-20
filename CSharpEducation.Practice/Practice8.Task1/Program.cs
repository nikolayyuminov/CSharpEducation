namespace Practice8.Task1;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task1!");
    
    Console.Write("Введите список чисел через пробел: ");
    List<int> numbers = Console.ReadLine()
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(int.Parse)
      .ToList();
    
    Console.WriteLine("Выберите порядок сортировки:");
    Console.WriteLine("1 - По возрастанию");
    Console.WriteLine("2 - По убыванию");

    string choice = Console.ReadLine();

    Comparison<int> comparison;

    switch (choice)
    {
      case "1":
        comparison = CompareAscending;
        break;
      case "2":
        comparison = CompareDescending;
        break;
      default:
        Console.WriteLine("Неверный выбор!");
        return;
    }
    
    numbers.Sort(comparison);
    foreach (var number in numbers)
    {
      Console.Write($"{number} ");
    }
  }
  
  public static int CompareAscending(int a, int b) => a < b ? -1 : 1;
  
  public static int CompareDescending(int a, int b) => a > b ? -1 : 1;
}