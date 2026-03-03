using GraphicsEngine.Primitives.Utils;

namespace GraphicsEngine.Primitives;

public class Point2D
{
    public float X { get; set; } = 0f;
    public float Y { get; set; } = 0f;


    public Point2D() { }

    public Point2D(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Point2D operator + (Point2D p0, Point2D p1)
    {
        return new Point2D(p0.X + p1.X, p0.Y + p1.Y);
    }

    public static Point2D operator * (float k, Point2D p)
    {
        return new Point2D(p.X * k, p.Y * k);
    }

    public static Point2D operator * (Point2D p, float k)
    {
        return k * p;
    }

    public static Point2D operator - (Point2D p0, Point2D p1)
    {
        return p0 + -1f * p1;
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

    public Point2D RoundedUp(int decimals)
    {
        return new Point2D(
            FloatUtils.RoundUp(X, decimals),
            FloatUtils.RoundUp(Y, decimals)
        );
    }

    public Point2D RoundedDown(int decimals)
    {
        return new Point2D(
            FloatUtils.RoundDown(X, decimals),
            FloatUtils.RoundDown(Y, decimals)
        );
    }

    public override bool Equals(object? obj)
    {
        return obj is Point2D d &&
               X == d.X &&
               Y == d.Y;
    }

    public override string ToString()
    {
        return $"Point2D:<{X},{Y}>";
    }
}
