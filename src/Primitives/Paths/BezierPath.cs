namespace GraphicsEngine.Primitives.Paths;

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
}
