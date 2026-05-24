namespace Practice6.Task7;

public class Circle :Shape
{
  public override double CalculateArea(double pi, double radius)
  {
    return pi * radius * radius;
  }
}