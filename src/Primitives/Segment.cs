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
}
