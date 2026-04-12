namespace Practice2.Task20;

class Program
{
  static void Main(string[] args)
  {
    
    Console.WriteLine("Задача 20!");
    int[,] mass = Massive(6,6);
    PrintMassive(mass);
    Console.WriteLine();
    Console.WriteLine(DifferenceMaxMin(mass));

  }
  
  static int [,] Massive(int row, int col)
  {
    var random = new Random();
    int [,] mass = new int[row, col];
    for (int i = 0; i < row; i++)
    {
      for (int j = 0; j < col; j++)
      {
        mass[i, j] = random.Next(1,101);
      }
    }
    return mass;
  }

  static int DifferenceMaxMin(int[,] mass)
  {
    var max = 0;
    var min = 100;
    for (int i = 0; i < mass.GetLength(0); i++)
    {
      for (int j = 0; j < mass.GetLength(1); j++)
      {
        if  (mass[i, j] > max)
        {
          max = mass[i, j];
        }
        else if (mass[i, j] < min)
        {
          min = mass[i, j];
        }
      }
    }
    return max - min;
  }

  static void PrintMassive(int[,] mass)
  {
    for (int i = 0; i < mass.GetLength(0); i++)
    {
      for (int j = 0; j < mass.GetLength(1); j++)
      {
        Console.Write(mass[i, j] + " ");
      }
      Console.WriteLine();
    }
  }
}