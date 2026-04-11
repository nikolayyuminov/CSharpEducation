namespace ConsoleApp1;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Hello, World!");
    int i = 0;
    for (int i1=0; i1 < 10; i1++)
    {
      Console.WriteLine($"{i1} из цикла for");
    }

    while (i < 10)
    {
      Console.WriteLine($"{i} из цикла while");
      i++;
    }

    var i2 = 0;
    do{
      Console.WriteLine($"{i2} из цикла do-while");
      i2++;
    }
    while (i2 < 10);

    string sentence = "";
    for (int words = 0; words < 3; words++)
    {
      Console.Write("Введите слово: ");
      var word = Console.ReadLine();
      sentence = sentence + word + " ";
    }
    Console.WriteLine($"Цикл for Вы ввели: {sentence}");

    int countSentenceWhile = 0;
    string sentenceWhile = "";
    while (countSentenceWhile < 3)
    {
      Console.Write("Введите слово: ");
      var word = Console.ReadLine();
      sentenceWhile = sentenceWhile + word + " ";
      countSentenceWhile++;
    }
    Console.WriteLine($"Цикл While Вы ввели: {sentenceWhile}");
    
    int countSentenceDoWhile = 0;
    string sentenceDoWhile = "";
    while (countSentenceDoWhile < 3)
    {
      Console.Write("Введите слово: ");
      var word = Console.ReadLine();
      sentenceDoWhile = sentenceDoWhile + word + " ";
      countSentenceDoWhile++;
    }
    Console.WriteLine($"Цикл do-While Вы ввели: {sentenceDoWhile}");
    
    
  }
}