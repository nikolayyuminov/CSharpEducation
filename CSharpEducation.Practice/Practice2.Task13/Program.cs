namespace Practice2.Task13;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 13!");
    var matrix = CreateMatrix(5, 5);
    PrintMassive(matrix);

  }
  
  static int [,] CreateMatrix(int rows, int cols)
  {
    var random = new Random();
    var matrix = new int[rows, cols];
    foreach (var row in Enumerable.Range(0, rows))
    {
      foreach (var col in Enumerable.Range(0, cols))
      {
        matrix[row, col] = random.Next();
      }
    }
    return matrix;
  }

  static void PrintMassive(int[,] matrix)
  {
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
      {
        Console.Write($"{matrix[i, j]} ");
      }
      Console.WriteLine();
    }
  }
}