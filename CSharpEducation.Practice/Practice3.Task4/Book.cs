namespace Practice3.Task4;

public class Book
{
  public string Title;
  public string Author;
  
  public Book(string title, string author)
  {
    Title = title;
    Author = author;
  }
  
  public Book()
  {
    Title = "Default";
    Author = "NoName";
  }
}