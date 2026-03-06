namespace GraphicsEngine;

public class Vector2
{
    public float X { get; set; } = 0f;
    public float Y { get; set; } = 0f;

    public float Magnitude => MathF.Sqrt(MathF.Pow(X, 2) + MathF.Pow(Y, 2));


    #region Operators
    public static Vector2 operator +(Vector2 p0, Vector2 p1)
        => new Vector2(p0.X + p1.X, p0.Y + p1.Y);

    public static Vector2 operator *(float k, Vector2 p)
    {
        return new Vector2(p.X * k, p.Y * k);
    }

    public static Vector2 operator *(Vector2 p, float k)
        => k * p;

    public static Vector2 operator -(Vector2 p0, Vector2 p1)
        => p0 + -1f * p1;
    #endregion

    #region Static Methods
    public static float Angle(Vector2 v1, Vector2 v2)
        => MathF.Acos(Dot(v1, v2) / (v1.Magnitude * v2.Magnitude));

    public static float Dot(Vector2 v1, Vector2 v2)
        => v1.X * v2.X + v1.Y * v2.Y;

    public static float Distance(Vector2 v1, Vector2 v2)
        => MathF.Sqrt(DistanceSquared(v1, v2));

    public static float DistanceSquared(Vector2 v1, Vector2 v2)
        => MathF.Pow(v2.X - v1.X, 2) + MathF.Pow(v2.Y - v2.Y, 2);

    #endregion


    public Vector2() { }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object? obj)
        => obj is Vector2 d && X == d.X && Y == d.Y;

    public override string ToString()
        => $"Vector2:<{X},{Y}>";
}
