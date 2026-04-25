namespace Practice3.Task6;

public struct Point
{
  public static int X;
  public static int Y;
  
  public static int DistanceBetweenPoints(int x, int y)
  {
    X = x;
    Y = y;
    return Math.Abs(Y - X);
  }
}