namespace Practice3.Task10;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 10!");

    var auto = new Automobile()
    {
      Mark = "Volvo"
    };
    print(auto);
  }
  
  public static void print(Automobile automobile)
  {
    Console.WriteLine($"Марка автомобиля: {automobile.Mark}");
  }
}