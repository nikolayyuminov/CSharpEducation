namespace Practice3.Task2;

public class Student
{
  public string Name;
  public int Age;
  private int _average;

  public int Average
  {
    get => _average;
    set
    {
      if (value >= 0 && value <= 5)
      {
        _average = value;
      }
      else
      {
        throw  new ArgumentOutOfRangeException();
      }
    }
  }

  public void PrintStudentInfo()
  {
    Console.WriteLine($"Name: {Name}");
    Console.WriteLine($"Age: {Age}");
  }
}