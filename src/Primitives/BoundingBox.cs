using static System.MathF;

namespace GraphicsEngine.Primitives.Interfaces;

/// <summary>
/// An Axis Aligned Bounding Box (AABB)
/// </summary>
public struct BoundingBox
{
    public readonly Vector2 Min;
    public readonly Vector2 Center;
    public readonly Vector2 Max;


    /// <summary>
    /// Offsets a bounding box
    /// </summary>
    public static BoundingBox operator + (BoundingBox b, Vector2 offset)
        => new BoundingBox([b.Min + offset, b.Max + offset]);

    /// <summary>
    /// Scales a bounding box
    /// </summary>
    public static BoundingBox operator * (BoundingBox b, float scale)
        => new BoundingBox([b.Min * scale, b.Max * scale]);


    public BoundingBox()
    {
        Min = new Vector2();
        Center = new Vector2();
        Max = new Vector2();
    }

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

    public bool Contains(Vector2 p)
        => (Min.X <= p.X && p.X <= Max.X) && (Min.Y <= p.Y && p.Y >= Max.Y);
}
