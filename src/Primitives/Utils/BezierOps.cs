using GraphicsEngine.Primitives.Interfaces;
using static GraphicsEngine.Primitives.Utils.FloatUtils;

namespace GraphicsEngine.Primitives.Utils;

public static class BezierOps
{
    private const int DEFAULT_MIN_SAMPLES = 20;


    #region distance from point to curve
    public static IDistanceToCurveResult ClosestPosition(ICubicSegment s, Vector2 Q, int minSamples = DEFAULT_MIN_SAMPLES)
    {
        return ClosestPosition(s.P0, s.P1, s.P2, s.P3, Q);
    }

    public static IDistanceToCurveResult ClosestPosition(Vector2 P0, Vector2 P1, Vector2 P2, Vector2 P3, Vector2 Q, int minSamples = DEFAULT_MIN_SAMPLES)
    {
        float tBest = 0f;
        float minDist2 = float.MaxValue;
        
        float t;

        // sample lightly looking for good a candidate sector for then refining with Newton-Raphson
        for (int i = 0; i <= minSamples; i++)
        {
            t = i / (float)minSamples;
            Vector2 p = BezierCurveFormulas.Cubic(P0, P1, P2, P3, t);
            float dist2 = Vector2.DistanceSquared(Q, p);

            if (dist2 < minDist2)
            {
                minDist2 = dist2;
                tBest = t;
            }
        }

        // Refine with Newton-Raphson
        t = tBest;

        for (int i = 0; i < 5; i++) // 5 iterations should be enouugh
        {
            Vector2 p = BezierCurveFormulas.Cubic(P0, P1, P2, P3, t);
            Vector2 dp = BezierCurveFormulas.CubicPrime(P0, P1, P2, P3, t);
            Vector2 ddp = BezierCurveFormulas.CubicSecond(P0, P1, P2, P3, t);

            float g = Vector2.Dot(p - Q, dp);
            float gPrime = Vector2.Dot(dp, dp) + Vector2.Dot(p - Q, ddp);

            if (Math.Abs(gPrime) < 1e-6f) break; // avoids dividing by cero

            t -= g / gPrime;
            t = Clamp01(t); // ensures it is within the interval
        }

        Vector2 closestPoint = BezierCurveFormulas.Cubic(P0, P1, P2, P3, t);
        float distance = Vector2.Distance(Q, closestPoint);

        return new DistanceToCurveResult(t, closestPoint, distance);
    }
    #endregion

}
