namespace Task1;

class Program
{
  static void Main(string[] args)
  {
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("   0   1   2");

    for (int i = 0; i < 3; i++)
    {
      Console.Write(" " + i + " ");

      for (int j = 0; j < 3; j++)
      {
        Console.Write(" ");

        if (board[i, j] == 'X')
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write('X');
        }
        else if (board[i, j] == 'O')
        {
          Console.ForegroundColor = ConsoleColor.Cyan;
          Console.Write('O');
        }
        else
        {
          Console.ForegroundColor = ConsoleColor.DarkGray;
          Console.Write(' ');
        }

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(" ");

        if (j < 2)
          Console.Write("|");
      }

      Console.WriteLine();

      if (i < 2)
        Console.WriteLine("   ---+---+---");
    }

    Console.ResetColor();
  }
}