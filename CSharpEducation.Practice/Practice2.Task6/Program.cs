namespace Practice2.Task6;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 6!");
    if (args.Length == 0)
    {
      Console.Write("Введите строку: ");
      string input = Console.ReadLine();
      Console.Write("Введите искомый символ: ");
      char inputSymbol = char.Parse(Console.ReadLine());
      Console.WriteLine(Percentage(input, inputSymbol));
    }
    else
    {
      Console.WriteLine(Percentage(args[0], char.Parse(args[1])));
    }
  }

  static double Percentage(string args, char symbol)
  {
    int counter = 0;
    if (string.IsNullOrWhiteSpace(args))
    {
      args = "TestString";
      symbol = 't';
      counter = args.Count(c => c == symbol);

      return (double)counter * 100 / args.Length;

    }
    counter = args.Count(c => c == symbol);

    return (double)counter * 100 / args.Length;
  }
}