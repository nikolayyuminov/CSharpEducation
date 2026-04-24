namespace Practice3.Task9;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 9!");

    var student = new Student() { Age = 31, Name = "Anton" };
    student.print();
    
    Student.Anonim(student);
    
    student.print();
  }
}