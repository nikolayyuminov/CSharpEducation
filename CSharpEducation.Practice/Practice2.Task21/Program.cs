namespace Practice2.Task21;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 21!");
    var random = new Random();
    do
    {
      Console.Write("Угадай число: ");
      if (int.TryParse(Console.ReadLine(), out var number))
      {
        Console.WriteLine(number);
        var luck = random.Next(1, 101);
        Console.WriteLine(luck);
        if (number != luck)
        {
          Console.WriteLine("Не повезло, сыграем еще раз?(y/n)");
          var answer = Console.ReadLine();
          if (answer == "y")
          {
            continue;
          }
          else
          {
            break;
          }
        }
        else
        {
          Console.WriteLine("Поздравляем, вы выиграли, сыграем еще раз?(y/n)");
          var answer = Console.ReadLine();
          if (answer == "y")
          {
            continue;
          }
          else
          {
            break;
          }
        }
      }
      else
      {
        throw new Exception("Пустой ввод");
      }
    }while (true);

    
  }
}