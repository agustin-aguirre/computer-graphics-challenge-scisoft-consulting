using GraphicsEngine.Primitives.Interfaces;
using static GraphicsEngine.Primitives.Utils.FloatUtils;


namespace GraphicsEngine.Primitives;

public class LinearBezierSegment : IBezierCurve
{
    public Vector2 P0 { get; set; }
    public Vector2 P1 { get; set; }

    public LinearBezierSegment(Vector2 p0, Vector2 p1)
    {
        P0 = p0;
        P1 = p1;
    }


    public Vector2 Evaluate(float t)
    {
        return GetEvaluator()(t);
    }

    protected virtual Func<float, Vector2> GetEvaluator()
    {
        return (float t) => P0 + Clamp01(t) * (P1 - P0); ;
    }
}
