namespace Practice2Task18;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 18!");

    int[] negativeMass = [45, 6, -4125, 331, -56, -54, -26];
    PrintMassive(negativeMass);
    int[] mass = DeleteNegativeNumber(negativeMass, out int counter);
    
    Console.WriteLine();
    PrintMassive(mass);
    Console.WriteLine();
    Console.WriteLine(counter);
  }
  
  static int[] DeleteNegativeNumber(int [] massive, out int countDelItem)
  {
    countDelItem = 0;
    for (int i = 0; i < massive.Length; i++)
    {
      if  (massive[i] < 0)
      {
        massive[i] = 0;
        countDelItem++;
      }
      
    } 
    return massive;
  }
  
  static void PrintMassive(int[] mass)
  {
    foreach (var row in mass)
    {
      Console.Write($"{row} ");
    }
  }
}