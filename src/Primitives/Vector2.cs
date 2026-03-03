using GraphicsEngine.Primitives.Utils;
using static GraphicsEngine.Primitives.Utils.FloatUtils;

namespace GraphicsEngine.Primitives;

public class Vector2
{
    public float X { get; set; } = 0f;
    public float Y { get; set; } = 0f;

    public float Magnitude => MathF.Sqrt(Square(X) + Square(Y));

    public static Vector2 operator +(Vector2 p0, Vector2 p1)
    {
        return new Vector2(p0.X + p1.X, p0.Y + p1.Y);
    }

    public static Vector2 operator *(float k, Vector2 p)
    {
        return new Vector2(p.X * k, p.Y * k);
    }

    public static Vector2 operator *(Vector2 p, float k)
    {
        return k * p;
    }

    public static Vector2 operator -(Vector2 p0, Vector2 p1)
    {
        return p0 + -1f * p1;
    }

    public static float Angle(Vector2 v1, Vector2 v2)
    {
        return MathF.Atan2(v2.X - v1.X, v2.Y - v1.Y);
    }

    public static float Dot(Vector2 v1, Vector2 v2)
    {
        return v1.Magnitude * v2.Magnitude * MathF.Cos(Angle(v1, v2));
    }


    public Vector2() { }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public void RoundUp(int decimals)
    {
        var roundedPoint = RoundedUp(decimals);
        X = roundedPoint.X;
        Y = roundedPoint.Y;
    }

    public void RoundDown(int decimals)
    {
        var roundedPoint = RoundedDown(decimals);
        X = roundedPoint.X;
        Y = roundedPoint.Y;
    }

    public Vector2 RoundedUp(int decimals)
    {
        return new Vector2(
            FloatUtils.RoundUp(X, decimals),
            FloatUtils.RoundUp(Y, decimals)
        );
    }

    public Vector2 RoundedDown(int decimals)
    {
        return new Vector2(
            FloatUtils.RoundDown(X, decimals),
            FloatUtils.RoundDown(Y, decimals)
        );
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2 d &&
               X == d.X &&
               Y == d.Y;
    }

    public override string ToString()
    {
        return $"Point2D:<{X},{Y}>";
    }
}
