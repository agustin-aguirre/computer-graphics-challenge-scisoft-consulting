using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives.Paths;

public class BezierSubpathVertexNode : IBezierSubpathNode
{
    public Vector2 Vertex { get; private set; }

    public float Length
        => 0;

    public float Area 
        => 0;

    public BoundingBox Bounds 
        => new BoundingBox() + Vertex;

    public CurveOrientation Orientation 
        => CurveOrientation.None;

    public BezierSubpathVertexNode(Vector2 vertex)
    {
        Vertex = vertex;
    }

    public IBezierSubpathNode Copy()
        => new BezierSubpathVertexNode(Vertex);

    public CurvePosition Sample(float t)
        => new CurvePosition()
        {
            T = t,
            Point = Vertex
        };

    public CurvePosition SampleDerivative(float t)
        => new CurvePosition
        {
            T = t,
            Point = new Vector2(0f, 0f)
        };

    public CurvePosition SampleSecondDerivative(float t)
        => SampleDerivative(t);

    public (float, CurvePosition) AproximateClosestPosition(Vector2 p, int samples)
        => ClosestPosition(p);

    public (float, CurvePosition) ClosestPosition(Vector2 p)
        => (Vector2.Distance(Vertex, p), Sample(0));

    public (float, CurvePosition, CurvePosition) ClosestPositions(IBezierSubpathNode subpath)
    {
        var (distance, subpathPos) = subpath.ClosestPosition(Vertex);
        return (distance, Sample(0), subpathPos);
    }

    public float CalcGradient(Vector2 direction, float t)
        => 0f;

    public float CalcGradientPrime(Vector2 direction, float t)
        => 0f;

    public CurvePosition[] Intersect(ISegment segment)
    {
        throw new NotImplementedException();
    }

    public float PointOnSegment(Vector2 p)
        => Vertex == p ? 0 : -1;
}
