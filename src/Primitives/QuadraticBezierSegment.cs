using static GraphicsEngine.Primitives.Utils.FloatUtils;


namespace GraphicsEngine.Primitives;

public class QuadraticBezierSegment : LinearBezierSegment
{
    public Vector2 P2 { get; set; }

    public QuadraticBezierSegment(Vector2 p0, Vector2 p1, Vector2 p2) : base(p0, p1)
    {
        P2 = p2;
    }


    protected override Func<float, Vector2> GetEvaluator()
    {
        return (float t) =>
        {
            t = Clamp01(t);
            return Square(1f-t)*P0 + 2f*(1f-t)*t*P1 + Square(t)*P2;
        };
    }
}
