using GraphicsEngine.Primitives.Interfaces;
using GraphicsEngine.Primitives.Utils;

namespace GraphicsEngine.Primitives;

public class LinearBezier : ISegment, IBezierCurve
{
    public Vector2 P0 { get; set; }
    public Vector2 P1 { get; set; }


    public LinearBezier(ISegment s)
    {
        P0 = s.P0;
        P1 = s.P1;
    }

    public LinearBezier(Vector2 p0, Vector2 p1)
    {
        P0 = p0;
        P1 = p1;
    }


    public Vector2 GetCurvePointAt(float t)
    {
        return BezierCurveFormulas.Linear(this, t);
    }
}
