namespace Practice3.Task5;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 5!");

    Book book1 = new Book("Title");
    Book book2 = new Book("Title", "Author");
    Book book3 = new Book("Title", "Author", 1948);

    book1.PrintBook();
    book2.PrintBook();
    book3.PrintBook();
  }
}