namespace Practice6Task4;

public class Meter
{
  public double meters { get; set; }

  public Meter(double meters)
  {
    this.meters = meters;
  }
  
  public static implicit operator Kilometer(Meter meters)
  {
    return new Kilometer(meters.meters / 1000);
  }
  
  public static explicit operator Meter(Kilometer kilometer)
  {
    return new Meter(kilometer.kilometers * 1000);
  }
}