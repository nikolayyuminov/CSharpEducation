using System;
using Practice4.MathHelper;


namespace CSharpEducation.LibraryPractice;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Подключение библиотек!");
    
    Console.WriteLine($"Сложение: {MathHelper.Addition(5, 5)}");
    
    Console.WriteLine($"Деление: {MathHelper.Division(5, 5)}");
    
    Console.WriteLine($"Умножение: {MathHelper.Multiplication(5, 5)}");
    
    Console.WriteLine($"Вычитаение: {MathHelper.Subtraction(5, 5)}");
    
    Console.WriteLine("Работа с персоной!");
    
    var person = new Person() {Name = "John Doe", Age = 20};
    
    person.PrintPerson();
  }
}