using System;

namespace Practice6Task4;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Task 4!");
    
    Meter meter = new Meter(153432210);
    Kilometer kilometer = meter;
    Console.WriteLine(kilometer.kilometers);
    Meter meter2 = (Meter)kilometer;
    Console.WriteLine(meter2.meters);
  }
}