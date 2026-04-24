namespace Practice3.Task11;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Задача 11!");
    
    var book = new Book(){ Author = "Semen", Title = "NeverWinterNights"};

    Console.WriteLine(BookInfo(book));
  }

  public static string BookInfo(Book book)
  {
    return $"Автор книги: {book.Author}, Название книги: {book.Title}";
  }
}