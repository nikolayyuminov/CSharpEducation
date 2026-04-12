namespace Practice2.Task14;

class Program
{
  static void Main(string[] args)
  {
    
    Console.WriteLine("Задача 15!");
    int[] mass = Massive(5);
    int[] invertMass = InvertMassive(mass);
    PrintMassive(mass);
    Console.WriteLine();
    PrintMassive(invertMass);
  }

  static int [] Massive(int range)
  {
    var random = new Random();
    int [] mass = new int[range];
    for (int i = 0; i < mass.Length; i++)
    {
      mass[i] = random.Next();
    }
    return mass;
  }

  static void PrintMassive(int[] mass)
  {
    foreach (var row in mass)
    {
      Console.Write($"{row} ");
    }
  }

  static int[] InvertMassive(int[] mass)
  {
    int[] invert = new int [mass.Length];
    for (int i = mass.Length-1; i >=0; i--)
    {
      invert[mass.Length-i-1] = mass[i];
    }
    return invert;
  }
}