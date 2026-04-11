using System;

namespace Task.CSharpBase;

class Program
{
  static void Main(string[] args)
  {
    const double Pi = 3.14;
    int age;
    string name;
    string companyName;
    bool isThisTrue;
    double weight;

    int myAge = 35;
    string myName = "Nikolay";
    double temperature = 15.0;
    bool isFemale = false;

    EnterTheName();
    EnterTwoPphrase();
    AreaOfCircle();
    Pifagor();
  }

  static void EnterTheName()
  {
    Console.Write($"Введите ваше имя: ");
    var name = Console.ReadLine();
    Console.WriteLine($"Привет, {name}!");
  }

  static void EnterTwoPphrase()
  {
    Console.Write("Введите первую фразу: ");
    var phrase1= Console.ReadLine();
    Console.Write("Введите вторую фразу: ");
    var phrase2 = Console.ReadLine();
    Console.WriteLine($"{phrase1} {phrase2}"); 
  }

  static void AreaOfCircle()
  {
    const double pi = 3.14;
    double radius = 65.2;
    double area = pi * Math.Pow(radius, 2);
    Console.WriteLine($"Площадь круга = {area} ");
  }

  static void Pifagor()
  {
    double leg1 = 5;
    double leg2 = 0;
    double hypotenuse = 13;
    if (hypotenuse == 0)
    {
      hypotenuse = Math.Sqrt(Math.Pow(leg1, 2) + Math.Pow(leg2, 2));
      Console.WriteLine($"Катет 1: {leg1:F2}, Катет 2: {leg2:F2}, Гипотенуза: {hypotenuse:F2}");
    }
    else if (leg1 == 0)
    {
      leg1 = Math.Sqrt(Math.Pow(hypotenuse, 2) - Math.Pow(leg2, 2));
      Console.WriteLine($"Катет 1: {leg1:F2}, Катет 2: {leg2:F2}, Гипотенуза: {hypotenuse:F2}");
    }
    else if (leg2 == 0)
    {
      leg2 = Math.Sqrt(Math.Pow(hypotenuse, 2) - Math.Pow(leg1, 2));
      Console.WriteLine($"Катет 1: {leg1:F2}, Катет 2: {leg2:F2}, Гипотенуза: {hypotenuse:F2}");
    }
    else
    {
      Console.WriteLine($"Катет 1: {leg1:F2}, Катет 2: {leg2:F2}, Гипотенуза: {hypotenuse:F2}");
    }
  }
}