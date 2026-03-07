using GraphicsEngine.Primitives.Interfaces;
using GraphicsEngine.Primitives.Math;

namespace GraphicsEngine.Primitives;

public class CubicBezierCurve
{
    public readonly ISegment4 Segment;

    public float Length
    {
        get
        {
            // Applies Gauss-Legendre
            // .5 * sum(w_i * |B'(x_i)|)
            return .5f * Calculus.GAUSS_LEGENDER_N5_PWs.Sum((pw) =>
            {
                float transformedT = (pw.Item1 + 1f) * .5f;
                return pw.Item2 * SamplePrime(transformedT).Point.Magnitude;
            });
        }
    }

    public float Area
        => (1f / 20) * (
            Vector2.Cross(Segment.P0, Segment.P1) +
            3f * Vector2.Cross(Segment.P0, Segment.P2) +
            6f * Vector2.Cross(Segment.P0, Segment.P3) +
            3f * Vector2.Cross(Segment.P1, Segment.P2) +
            3f * Vector2.Cross(Segment.P1, Segment.P3) +
            Vector2.Cross(Segment.P2, Segment.P3)
        );

    public float Orientation
        => MathF.Sign(Area);

    public CubicBezierCurve(ISegment4 segment)
    {
        Segment = segment;
    }

    public CurvePosition Sample(float t)
        => sample(Bezier.CUBIC, t);

    public IEnumerable<CurvePosition> Sample(int samples)
    {
        for (int i = 0; i < samples; i++)
        {
            float t = i / samples;
            yield return Sample(t);
        }
    }

    public CurvePosition SamplePrime(float t)
        => sample(Bezier.CUBIC_PRIME, t);

    public CurvePosition SampleSecond(float t)
        => sample(Bezier.CUBIC_SECOND, t);


    private CurvePosition sample(Func<ISegment4, float, Vector2> formula, float t)
    {
        t = t.Clamp01();
        return new CurvePosition
        {
            T = t,
            Point = formula(Segment, t)
        };
    }
}
