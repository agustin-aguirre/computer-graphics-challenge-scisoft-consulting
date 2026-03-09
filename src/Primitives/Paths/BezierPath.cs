namespace GraphicsEngine.Primitives.Paths;

/// <summary>
/// A bezier path composed of subpaths
/// </summary>
public class BezierPath
{
    public readonly List<BezierSubpath> Subpaths;

    public BezierPath()
    {
        Subpaths = new List<BezierSubpath>();
    }

    public void Copy(int index)
    {
        var newNode = Subpaths.ElementAt(index).Copy();
        Subpaths.Add(newNode);
    }

    public void Move(int index, int offset)
    {
        if (offset + index > Subpaths.Count)
            throw new ArgumentOutOfRangeException();
        BezierSubpath node = Subpaths.ElementAt(index);
        Subpaths.RemoveAt(index);
        Subpaths.Insert(index + offset, node);
    }

    public void Move(BezierSubpath subpath, int offset)
        => Move(Subpaths.IndexOf(subpath), offset);

    public void Forward(int index)
        => Move(index, 1);

    public void Forward(BezierSubpath subpath)
        => Move(subpath, 1);

    public void Backward(int index)
        => Move(index, -1);

    public void Backward(BezierSubpath subpath)
        => Move(subpath, -1);
}
