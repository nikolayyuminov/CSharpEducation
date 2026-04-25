namespace Practice3.Task8;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 8!");
    
    var rect =  new Rectangle(){height = 12, width = 32};
    
    Console.WriteLine(rect.AreaOfRectangle());
  }
}