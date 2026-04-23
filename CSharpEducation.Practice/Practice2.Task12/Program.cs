namespace Practice2.Task12;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 12!");
    Console.WriteLine("Для вывода всех строк введите exit");
    string input = "";
    string result ="";
    do
    {
      Console.Write("Введите строку: ");
      input = Console.ReadLine();
      if (input == "exit")
      {
        break;
      }
      result += $"{input} ";
    } while (true);
    Console.WriteLine(result);
  }
}