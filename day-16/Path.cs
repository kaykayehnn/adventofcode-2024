public class Path : IComparable<Path>
{
  private int direction = 0;
  public static Vector GetDirectionVector(int direction)
  {
    int remainder = direction % 4;

    if (remainder == 0)
    {
      return new Vector(0, 1);
    }
    if (remainder == 1)
    {
      return new Vector(1, 0);
    }
    if (remainder == 2)
    {
      return new Vector(0, -1);
    }
    if (remainder == 3)
    {
      return new Vector(-1, 0);
    }
    throw new Exception();
  }

  public int I { get; set; }
  public int J { get; set; }
  public int Direction
  {
    get { return this.direction; }
    set
    {
      this.direction = (4 + value) % 4;
    }
  }
  private int myVar;
  public int MyProperty
  {
    get { return myVar; }
    set { myVar = value; }
  }

  public int Score { get; set; }

  public int CompareTo(Path? other)
  {
    int cmp = this.Score.CompareTo(other.Score);
    if (cmp == 0) cmp = this.I.CompareTo(other.I);
    if (cmp == 0) cmp = this.J.CompareTo(other.J);
    if (cmp == 0) cmp = this.Direction.CompareTo(other.Direction);
    return cmp;
  }
}