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

    public IntersectionResult[] Intersect(ISegment segment)
    {
        float tolerance = 1e-6f;

        Vector2 ab = segment.P1 - segment.P0;
        Vector2 ap = Vertex - segment.P1;

        // First, check that vertex is not colinear with segment (they don't share axis), therefore, it doesn't intersect
        float colinearity = Vector2.Cross(ap, ab);

        if (MathF.Abs(colinearity) > tolerance) return [];

        // Second Check segments range
        float dot = Vector2.Dot(ap, ab);
        
        // vertex shares axis but its outside of the segment (ex: p____a____b)
        if (dot < 0) return [];

        float abLength = Vector2.Dot(ab, ab);
        
        // vertex is outside of the segment (dot prod works as "length measure" since bot vector segments are not necessarily normalized)
        // in this case we check a____b____p (same direction, but outside of the segment)
        if (dot > abLength) return [];

        // finally, to obtain the normalized distance (between 0-1) at wich both intersect, we simply divide the dot product and the segment length
        return [
            new IntersectionResult {
                Points = [Vertex],
                Positions = [(0f, dot / abLength)]
            }
        ];
    }

    public PointInPathResult BoundaryPosition(Vector2 p)
        => Vertex.Equals(p)
            ? PointInPathResult.Along
            : PointInPathResult.Outside;
}
