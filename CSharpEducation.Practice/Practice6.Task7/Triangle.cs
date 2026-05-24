namespace Practice6.Task7;

public class Triangle : Shape
{
  public override double CalculateArea(double baseSide, double height)
  {
    return (baseSide * height) / 2;
  }
}