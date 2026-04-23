namespace Practice2.Task7;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 7!");
    Console.Write("Введите строку: ");
    string test = Console.ReadLine();
    Upper(test);
    Lower(test);
    FirstUpper(test);
    
  }

  static void Upper(string test)
  {
    Console.WriteLine($"Перевели в заглавные: {test.ToUpper()}");
  }
  
  static void Lower(string test)
  {
    Console.WriteLine($"Перевели в строчные: {test.ToLower()}");
  }
  
  static void FirstUpper(string test)
  {
    Console.Write($"Перевели первую букву в заглавную: {char.ToUpper(test[0])}{test.Substring(1)}");
  }
}