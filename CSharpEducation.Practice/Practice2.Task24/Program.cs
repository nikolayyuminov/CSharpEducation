namespace Practice2.Task24;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 24!");
    foreach(var day in Enum. GetValues<DayOfWeek>())
    {
      Console.WriteLine(day);
    }
  }

  enum DayOfWeek
  {
    Понедельник,
    Вторник,
    Среда,
    Четверг,
    Пятница,
    Суббота,
    Воскресенье
  }
}