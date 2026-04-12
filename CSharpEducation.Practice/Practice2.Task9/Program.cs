namespace Practice2.Task9;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 9!");
    var massive = CreateMassive(5);
    PrintMassive(massive);
  }

  static int[] CreateMassive(int range)
  {
    int [] massive = new int[range];
    for (int i = 0; i < range; i++)
    {
      massive[i] = i;
    }
    return massive;
  }

  static void PrintMassive(int[] massive)
  {
    foreach (var item in massive)
    {
      Console.Write($"{item} ");
    }
  }
}