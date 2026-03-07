using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives;


public class Segment : ISegment
{
    public Vector2 P0 { get; private set; } = new Vector2();
    public Vector2 P1 { get; private set; } = new Vector2();

    public Segment(Vector2 p0, Vector2 p1)
    {
        P0 = p0;
        P1 = p1;
    }

    public float Length
        => Vector2.Distance(P0, P1);

    public Vector2[] Points
        => [P0, P1];

    public IntersectionResult Intersect(ISegment other)
    {
        float tolerance = 1e-6f;

        Vector2 a2a1 = P1 - P0;    // own segment vector
        Vector2 b2b1 = other.P1 - other.P0;    // other segment vector

        // if very close to 0, then they're parallel lines
        float crossDeterminant = Vector2.Cross(a2a1, b2b1);
        
        bool theyAreParallel = crossDeterminant < tolerance;
        if (theyAreParallel) return new IntersectionResult();

        Vector2 b1a1 = other.P0 - P0;   // vector of the beginings of both segments

        float t = Vector2.Cross(b1a1, b2b1) / crossDeterminant;
        float u = Vector2.Cross(b1a1, a2a1) / crossDeterminant;

        // if t and u both are between 0 and 1, the there's an intersection
        if (t >= 0f && t <= 1f && u >= 0f && u <= 1f)
        {
            return new IntersectionResult()
            {
                Points = [P0 + t * a2a1],
                Positions = [(t, u)]
            };
        }

        return new IntersectionResult();
    }
}
