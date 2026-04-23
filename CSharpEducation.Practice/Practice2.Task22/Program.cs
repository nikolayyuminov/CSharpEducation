namespace Practice2.Task22;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 22!");
    var random = new Random();
    var secretNumber = random.Next(1, 101);
    for (int i =  0; i <= 10; i++)
    {
      Console.Write("Угадай число: ");
      if (int.TryParse(Console.ReadLine(), out var number))
      {
        if (i != 10)
        {
          if (number < secretNumber)
          {
            Console.WriteLine("Твое число меньше, попробуем еще раз?(y/n)");
            var answer = Console.ReadLine();
            if (answer == "y")
            {
              continue;
            }
            break;
          }
          if  (number > secretNumber)
          {
            Console.WriteLine("Твое число больше, попробуем еще раз?(y/n)");
            var answer = Console.ReadLine();
            if (answer == "y")
            {
              continue;
            }
            break;
          }
          else
          {
            Console.WriteLine("Поздравляем, вы выиграли!");
            var answer = Console.ReadLine();
            if (answer == "y")
            {
              continue;
            }
            break;
          }
        }
        Console.WriteLine("Попытки закончились, пока!");
      }
      else
      {
        Console.WriteLine("Пустой ввод");
      }
    }
  }
}