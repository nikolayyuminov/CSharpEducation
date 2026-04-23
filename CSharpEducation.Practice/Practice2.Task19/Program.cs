namespace Practice2.Task19;

class Program
{
  static void Main(string[] args)
  {
    
    Console.WriteLine("Задача 19!");
    int[] mass = Massive(5);
    PrintMassive(mass);
    Console.WriteLine();
    Console.WriteLine(DifferenceMaxMin(mass));

  }
  
  static int [] Massive(int range)
  {
    var random = new Random();
    int [] mass = new int[range];
    for (int i = 0; i < mass.Length; i++)
    {
      mass[i] = random. Next(1, 101);
    }
    return mass;
  }

  static int DifferenceMaxMin(int[] mass)
  {

    var max = mass.Max();
    var min = mass.Min();
    
    return max - min;
  }

  static void PrintMassive(int[] mass)
  {
    foreach (var item in mass)
    {
      Console.Write($"{item} ");
    }
  }
}