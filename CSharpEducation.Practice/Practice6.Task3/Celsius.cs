namespace Practice6.Task3;

public class Celsius
{
  public double degrees { get; set; }
  
  public Celsius(double degrees)
  {
    this.degrees = degrees;
  }
  public static implicit operator Fahrenheit(Celsius c)
  {
    return new Fahrenheit((c.degrees * 9 / 5) + 32);
  }
  
  public static explicit operator Celsius(Fahrenheit f)
  {
    return new Celsius((f.degrees - 32) * 5 / 9);
  }
  
}