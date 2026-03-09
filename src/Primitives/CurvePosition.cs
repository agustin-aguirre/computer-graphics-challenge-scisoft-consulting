namespace GraphicsEngine.Primitives;

/// <summary>
/// A point of a curve at a T distance (0 <= T <= 1)
/// </summary>
public struct CurvePosition
{
    public float T;
    public Vector2 Point;
}
