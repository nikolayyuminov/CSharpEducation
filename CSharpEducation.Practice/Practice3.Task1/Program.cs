namespace Practice3.Task1;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 1!");

    var student = new Student()
    {
      Age = 32,
      Name = "Zubrila"
    };
    student.PrintStudentInfo();
  }
}