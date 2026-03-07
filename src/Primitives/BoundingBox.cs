using static System.MathF;

namespace GraphicsEngine.Primitives.Interfaces;

/// <summary>
/// Axis Aligned Bounding Box (AABB)
/// </summary>
public struct BoundingBox
{
    public readonly Vector2 Min;
    public readonly Vector2 Center;
    public readonly Vector2 Max;

    public BoundingBox(IEnumerable<Vector2> points)
    {
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        foreach (var point in points)
        {
            minX = Min(point.X, minX);
            minY = Min(point.Y, minY);
            maxX = Max(point.X, maxX);
            maxX = Max(point.Y, maxY);
        }

        Min = new Vector2(minX, minY);
        Max = new Vector2(maxX, maxY);
        Center = (Min + Max) * .5f;
    }
}
