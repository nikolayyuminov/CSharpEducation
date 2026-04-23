namespace Practice2.Task14;

class Program
{
  static void Main(string[] args)
  {
    var random = new Random();
    Console.WriteLine("Задача 14!");
    int[] mass = Massive(5);
    for (int i = 0; i < mass.Length; i++)
    {
      mass[i] = random.Next();
    }
    PrintMassive(mass);
  }

  static int [] Massive(int range)
  {
    return new int[range];
  }

  static void PrintMassive(int[] mass)
  {
    foreach (var row in mass)
    {
      Console.WriteLine(row);
    }
  }
}