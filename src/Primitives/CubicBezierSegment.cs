using static System.MathF;
using static GraphicsEngine.Primitives.Utils.FloatUtils;


namespace GraphicsEngine.Primitives;

public class CubicBezierSegment : QuadraticBezierSegment
{
    public Point2D P3 { get; set; }

    public CubicBezierSegment(Point2D p0, Point2D p1, Point2D p2, Point2D p3) : base(p0, p1, p2)
    {
        P3 = p3;
    }


    protected override Func<float, Point2D> GetEvaluator()
    {
        return (float t) =>
        {
            t = Clamp01(t);
            return Pow(1f - t, 3f) * P0 + 3f * Square(1f - t) * t * P1 + 3f * (1f - t) * Square(t) * P2 + Pow(t, 3f) * P3;
        };
    }
}
