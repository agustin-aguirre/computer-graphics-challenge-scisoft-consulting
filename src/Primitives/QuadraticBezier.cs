using GraphicsEngine.Primitives.Interfaces;
using GraphicsEngine.Primitives.Utils;

namespace GraphicsEngine.Primitives;

public class QuadraticBezier : IQuadraticSegment, IBezierCurve
{
    public Vector2 P0 { get; set; }
    public Vector2 P1 { get; set; }
    public Vector2 P2 { get; set; }


    public QuadraticBezier(IQuadraticSegment s)
    {
        P0 = s.P0;
        P1 = s.P1;
        P2 = s.P2;
    }

    public QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2)
    {
        P0 = p0;
        P1 = p1;
        P2 = p2;
    }


    public Vector2 GetCurvePointAt(float t)
    {
        return BezierCurveFormulas.Quadratic(this, t);
    }
}
