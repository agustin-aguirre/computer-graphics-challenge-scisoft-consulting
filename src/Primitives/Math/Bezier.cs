using static System.MathF;
using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives.Math;

public static class Bezier
{
    public static Func<ISegment4, float, Vector2> CUBIC
        => (ISegment4 s, float t) =>
        {
            float u = 1 - t;
            // B(t)=(1-t)^3*P0+3(1-t)^2*t*P1+3(1-t)t^2*P2+t^3*P3
            return Pow(u, 3) * s.P0 + 3 * Pow(u, 2) * t * s.P1 + 3 * u * Pow(t, 2) * s.P2 + Pow(t, 3) * s.P3;
        };

    public static Func<ISegment4, float, Vector2> CUBIC_PRIME
        => (ISegment4 s, float t) =>
        {
            float u = 1 - t;
            // B′(t)=3(1−t)^2(P1​−P0​)+6(1−t)t(P2​−P1​)+3t^2(P3​−P2​)
            return 3 * Pow(u, 2) * (s.P1 - s.P0) + 6 * u * t * (s.P2 - s.P1) + 3 * Pow(t, 2) * (s.P3 - s.P2);
        };

    public static Func<ISegment4, float, Vector2> CUBIC_SECOND
        => (ISegment4 s, float t) =>
        {
            float u = 1 - t;
            // B′'(t)=6((1−t)*(P2-2*P1+P0)+t*(P3-2*P2+P1))
            return 6 * (u * (s.P2 - 2 * s.P1 + s.P0) + t * (s.P3 - 2 * s.P2 + s.P1));
        };
}
