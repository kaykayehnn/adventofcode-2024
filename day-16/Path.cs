public class Path : IComparable<Path>
{
  private static int UniqueIdCounter = 0;
  // This unique ID is used to differentiate between different routes that lead to the same cell.
  // It is only required for part 2 of the challenge.
  private int uniqueId;
  private int direction = 0;

  public Path()
  {
    this.uniqueId = Path.UniqueIdCounter++;
  }

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
  
  public Path Prev { get; set; }

  public int Score { get; set; }

  public int CompareTo(Path? other)
  {
    int cmp = this.Score.CompareTo(other.Score);
    if (cmp == 0) cmp = this.uniqueId.CompareTo(other.uniqueId);
    return cmp;
  }
}