using GraphicsEngine.Primitives.Interfaces;
using GraphicsEngine.Primitives.Utils;
using static GraphicsEngine.Primitives.Utils.FloatUtils;

namespace GraphicsEngine.Primitives;


public class CubicBezier : ICubicSegment, IBezierCurve
{
    public Vector2 P0 { get; set; }
    public Vector2 P1 { get; set; }
    public Vector2 P2 { get; set; }
    public Vector2 P3 { get; set; }


    public CubicBezier(ICubicSegment s)
    {
        P0 = s.P0;
        P1 = s.P1;
        P2 = s.P2;
        P3 = s.P3;
    }

    public CubicBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        P0 = p0;
        P1 = p1;
        P2 = p2;
        P3 = p3;
    }

    public Vector2 GetCurvePointAt(float t)
    {
        return BezierCurveFormulas.Cubic(this, Clamp01(t));
    }
}
