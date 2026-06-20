namespace Practice8.Task2;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task2!");
    
    Console.Write("Введите слова через пробел: ");
    List<string> strings = Console.ReadLine()
      .Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    
    Console.WriteLine("Выберите порядок сортировки:");
    Console.WriteLine("1 - Первый символ 'А'");
    Console.WriteLine("2 - Короче 5-ти символов");

    string choice = Console.ReadLine();

    Func<string, bool> func;

    switch (choice)
    {
      case "1":
        func = StartWithA;
        break;
      case "2":
        func = Str5Simbols;
        break;
      default:
        Console.WriteLine("Неверный выбор!");
        return;
    }
    
    var result = strings.Where(func);
    foreach (var _string in result)
    {
      Console.Write($"{_string} ");
    }
  }
  
  public static bool StartWithA(string str)
  {
    
    return str.ToLower().StartsWith('a');
  }
  
  public static bool Str5Simbols(string str)
  {
    return str.Length <= 5;
  }
}