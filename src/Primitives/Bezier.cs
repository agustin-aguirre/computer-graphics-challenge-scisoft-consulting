using static System.MathF;
using static GraphicsEngine.Primitives.Utils.FloatUtils;

namespace GraphicsEngine.Primitives;

public static class Bezier
{
    public static Vector2 Linear(LinearBezierSegment s, float t)
    {
        return s.P0 + Clamp01(t) * (s.P1 - s.P0);
    }

    public static Vector2 Quadratic(QuadraticBezierSegment s, float t)
    {
        t = Clamp01(t);
        float u = 1 - t;
        return Square(u) * s.P0 + 2 * u * t * s.P1 + Square(t) * s.P2;
    }

    public static Vector2 Cubic(CubicBezierSegment s, float t)
    {
        t = Clamp01(t);
        float u = 1 - t;
        return Pow(u, 3) * s.P0 + 3 * Square(u) * t * s.P1 + 3 * u * Square(t) * s.P2 + Pow(t, 3) * s.P3;
    }

    public static Vector2 CubicPrime(CubicBezierSegment s, float t)
    {
        t = Clamp01(t);
        float u = 1 - t;
        // B′(t)=3(1−t)^2(P1​−P0​)+6(1−t)t(P2​−P1​)+3t^2(P3​−P2​)
        return 3 * Square(u) * (s.P1 - s.P0) + 6 * u * t * (s.P2 - s.P1) + 3 * Square(t) * (s.P3 - s.P2);
    }

    public static Vector2 CubicSecond(CubicBezierSegment s, float t)
    {
        t = Clamp01(t);
        float u = 1 - t;
        // B′'(t)=6((1−t)*(P2-2*P1+P0)+t*(P3-2*P2+P1))
        return 6 * (u * (s.P2 - 2 * s.P1 + s.P0) + t * (s.P3 - 2 * s.P2 + s.P1));
    }
}
