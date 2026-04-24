namespace Practice3.Task4;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 4!");
    
    var book1 = new Book();
    var book2 = new Book("Name of book", "Author of book");

    Console.WriteLine(book1.Title);
    Console.WriteLine(book1.Author);
    Console.WriteLine(book2.Title);
    Console.WriteLine(book2.Author);
  }
}