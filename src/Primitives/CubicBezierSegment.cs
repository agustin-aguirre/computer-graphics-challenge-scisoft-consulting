using static System.MathF;
using static GraphicsEngine.Primitives.Utils.FloatUtils;


namespace GraphicsEngine.Primitives;

public class CubicBezierSegment : QuadraticBezierSegment
{
    public Vector2 P3 { get; set; }

    public CubicBezierSegment(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3) : base(p0, p1, p2)
    {
        P3 = p3;
    }


    protected override Func<float, Vector2> GetEvaluator()
    {
        return (float t) =>
        {
            t = Clamp01(t);
            return Pow(1f - t, 3f) * P0 + 3f * Square(1f - t) * t * P1 + 3f * (1f - t) * Square(t) * P2 + Pow(t, 3f) * P3;
        };
    }
}
