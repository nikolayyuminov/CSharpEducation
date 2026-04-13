namespace Practice2.Task25;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 25!");
    
    Console.Write("Введите номер дня недели: ");
    var result = Convert.ToInt32(Console.ReadLine());
    
    Console.WriteLine($"{(DayOfWeek)result}");
  }

  enum DayOfWeek
  {
    Понедельник = 1,
    Вторник,
    Среда,
    Четверг,
    Пятница,
    Суббота,
    Воскресенье
  }
}