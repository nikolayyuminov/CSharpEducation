namespace Practice3.Task9;

public class Student
{
  public string Name;
  public int Age;

  public static void Anonim(Student student)
  {
    student.Name = "Anonim";
  }
  
  public void print()
  {
    Console.WriteLine($"Name: {Name}");
    Console.WriteLine($"Age: {Age}");
  }
}