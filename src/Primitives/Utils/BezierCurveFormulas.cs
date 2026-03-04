using static System.MathF;
using GraphicsEngine.Primitives.Interfaces;
using static GraphicsEngine.Primitives.Utils.FloatUtils;

namespace GraphicsEngine.Primitives.Utils;

public static class BezierCurveFormulas
{
    public static Vector2 Linear(Vector2 P0, Vector2 P1, float t)
    {
        return P0 + Clamp01(t) * (P1 - P0);
    }

    public static Vector2 Linear(ISegment s, float t)
    {
        return Linear(s.P0, s.P1, t);
    }


    public static Vector2 Quadratic(Vector2 P0, Vector2 P1, Vector2 P2, float t)
    {
        t = Clamp01(t);
        float u = 1 - t;
        return Square(u) * P0 + 2 * u * t * P1 + Square(t) * P2;
    }

    public static Vector2 Quadratic(IQuadraticSegment s, float t)
    {
        return Quadratic(s.P0, s.P1, s.P2, t);
    }



    public static Vector2 Cubic(Vector2 P0, Vector2 P1, Vector2 P2, Vector2 P3, float t)
    {
        t = Clamp01(t);
        float u = 1 - t;
        return Pow(u, 3) * P0 + 3 * Square(u) * t * P1 + 3 * u * Square(t) * P2 + Pow(t, 3) * P3;
    }

    public static Vector2 Cubic(ICubicSegment s, float t)
    {
        return Cubic(s.P0, s.P1, s.P2, s.P3, t);
    }


    public static Vector2 CubicPrime(Vector2 P0, Vector2 P1, Vector2 P2, Vector2 P3, float t)
    {
        t = Clamp01(t);
        float u = 1 - t;
        // B′(t)=3(1−t)^2(P1​−P0​)+6(1−t)t(P2​−P1​)+3t^2(P3​−P2​)
        return 3 * Square(u) * (P1 - P0) + 6 * u * t * (P2 - P1) + 3 * Square(t) * (P3 - P2);
    }

    public static Vector2 CubicPrime(ICubicSegment s, float t)
    {
        return CubicPrime(s.P0, s.P1, s.P2, s.P3, t);
    }


    public static Vector2 CubicSecond(Vector2 P0, Vector2 P1, Vector2 P2, Vector2 P3, float t)
    {
        t = Clamp01(t);
        float u = 1 - t;
        // B′'(t)=6((1−t)*(P2-2*P1+P0)+t*(P3-2*P2+P1))
        return 6 * (u * (P2 - 2 * P1 + P0) + t * (P3 - 2 * P2 + P1));
    }

    public static Vector2 CubicSecond(ICubicSegment s, float t)
    {
        return CubicSecond(s.P0, s.P1, s.P2, s.P3, t);
    }
}
