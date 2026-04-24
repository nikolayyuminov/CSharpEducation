namespace Practice3.Task5;

public class Book
{
  public string Title;
  public string Author;
  public int Year;

  public void PrintBook()
  {
    Console.WriteLine($"Title: {Title}");
    Console.WriteLine($"Author: {Author}");
    Console.WriteLine($"Year: {Year}");
    Console.WriteLine();
  }
  
  public Book(string title, string author) : this(title)
  {
    Author = author;
  }
  
  public Book(string title)
  {
    Title = title;
  }
  public Book(string title, string author, int year) : this(title, author)
  {
    Year = year;
  }
}