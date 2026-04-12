namespace Practice2.Task9;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 11!");
    var massive = CreateMassive(int.Parse(args[0]), int.Parse(args[1]));
    PrintMassive(massive);
  }

  static int[] CreateMassive(int range, int firstElement)
  {
    int [] massive = new int[range];
    massive [0] = firstElement;
    for (int i = 1; i < range; i++)
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