using System;

namespace Practice4.MathHelper;

public class Person
{
  public string Name { get; set; }
  
  public int Age { get; set; }

  public void PrintPerson()
  {
    Console.WriteLine($"Name: {Name}, Age: {Age}");
  }
}