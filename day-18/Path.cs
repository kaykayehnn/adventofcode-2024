struct Path : IComparable<Path>
{
  public int X { get; set; }
  public int Y { get; set; }
  public int Steps { get; set; }

  public int CompareTo(Path other)
  {
    int cmp = this.Steps.CompareTo(other.Steps);
    if (cmp == 0) cmp = this.X.CompareTo(other.X);
    if (cmp == 0) cmp = this.Y.CompareTo(other.Y);
    return cmp;
  }
}