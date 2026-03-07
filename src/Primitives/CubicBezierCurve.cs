using GraphicsEngine.Primitives.Interfaces;
using GraphicsEngine.Primitives.Math;

namespace GraphicsEngine.Primitives;

public class CubicBezierCurve
{
    public ISegment4 Segment { get; private set; }

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
