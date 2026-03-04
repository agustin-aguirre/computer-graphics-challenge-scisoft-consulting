using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives.Utils;

public struct DistanceToCurveResult : IDistanceToCurveResult
{
    public float T { get; private set; }
    public Vector2 Point { get; private set; }
    public float Distance { get; private set; }
    
    public DistanceToCurveResult(float t, Vector2 point, float distance)
    {
        T = t;
        Point = point;
        Distance = distance;
    }
}
