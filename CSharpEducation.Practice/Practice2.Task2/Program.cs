namespace Practice2.Task2;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 2!");
    
    int km = MInKm(1000);
    int cm = KmInCm(2);
    int kmh = McInKmh(8);
    int degreeF = DegreeCInF(30);
    Console.WriteLine(km);
    Console.WriteLine(cm);
    Console.WriteLine(kmh);
    Console.WriteLine(degreeF);
  }

  static int MInKm(int m)
  {
    return m / 1000;
  }
  
  static int KmInCm(int km)
  {
    return km * 1000000;
  }
  
  static int McInKmh(int mc)
  {
    return mc * 3600/1000;
  }
  
  static int DegreeCInF(int degreeC)
  {
    return (degreeC * 9/5)+32;
  }
}