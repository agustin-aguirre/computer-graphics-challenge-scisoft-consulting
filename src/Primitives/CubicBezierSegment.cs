using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives;


/// <summary>
/// The combination of 4 segments, representing the control points of a Cubic Bezier Curve.
/// </summary>
public class CubicBezierSegment : ISegment4
{
    public Vector2 P0 { get; private set; }
    public Vector2 P1 { get; private set; }
    public Vector2 P2 { get; private set; }
    public Vector2 P3 { get; private set; }

    public Vector2[] Points
        => [P0, P1, P2, P3];

    public float Length
        => Vector2.Distance(P0, P1) + Vector2.Distance(P1, P2) + Vector2.Distance(P2, P3);

    public ISegment4 Reversed
        => new CubicBezierSegment(P3, P2, P1, P0);

    public ISegment S1
        => new Segment(P0, P1);
    public ISegment S2
        => new Segment(P1, P2);
    public ISegment S3
        => new Segment(P2, P3);

    public ISegment[] Segments 
        => [S1, S2, S3];

    public CubicBezierSegment(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        P0 = p0;
        P1 = p1;
        P2 = p2;
        P3 = p3;
    }

    public void Reverse()
    {
        P0 = P3;
        P1 = P2;
        P2 = P1;
        P3 = P0;
    }

    public IntersectionResult Intersect(ISegment other)
    {
        var points = new List<Vector2>();
        var positions = new List<(float, float)>();

        Segments.
            Select(s => s.Intersect(other)).
            Where(result => result.Points.Length > 0).
            Aggregate((points, positions), (acc, intersection) =>
            {
                acc.points.AddRange(intersection.Points);
                acc.positions.AddRange(intersection.Positions);
                return acc;
            });
        
        return new IntersectionResult()
        {
            Points = points.ToArray(),
            Positions = positions.ToArray()
        };
    }
}
