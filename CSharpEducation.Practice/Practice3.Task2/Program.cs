namespace Practice3.Task2;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 2!");

    var student = new Student()
    {
      Age = 32,
      Name = "Zubrila"
    };
    student.PrintStudentInfo();

    student.Average = 6;
    Console.WriteLine(student.Average);
  }
}