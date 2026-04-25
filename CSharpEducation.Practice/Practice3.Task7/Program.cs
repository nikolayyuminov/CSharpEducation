namespace Practice3.Task7;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 7!");

    Console.WriteLine(
      AmountDays(Month.february));
  }

  static int AmountDays(Month month)
  {
    if (month is Month.january or Month.march or Month.may or Month.july or Month.august or Month.october or Month.december) return 31;
    
    if (month is Month.february) return 28;

    if (month is Month.april or Month.june or Month.september or Month.november) return 30;
    return 0;
  }
}