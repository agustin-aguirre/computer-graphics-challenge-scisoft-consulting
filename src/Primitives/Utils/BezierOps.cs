using GraphicsEngine.Primitives.Interfaces;
using System.Runtime.Intrinsics.Arm;
using static GraphicsEngine.Primitives.Utils.FloatUtils;

namespace GraphicsEngine.Primitives.Utils;


public struct Closest2PointsResult
{
    public float Distance;
    public float S;
    public float T;
    public Vector2 B1Point;
    public Vector2 B2Point;
}

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
            Vector2 curvePoint = BezierCurveFormulas.Cubic(P0, P1, P2, P3, t);
            Vector2 tangentVector = BezierCurveFormulas.CubicPrime(P0, P1, P2, P3, t);
            Vector2 ddp = BezierCurveFormulas.CubicSecond(P0, P1, P2, P3, t);
            
            float g = Vector2.Dot(curvePoint - Q, tangentVector);
            float gPrime = Vector2.Dot(tangentVector, tangentVector) + Vector2.Dot(curvePoint - Q, ddp);

            if (Math.Abs(gPrime) < 1e-6f) break; // avoids dividing by cero

            t -= g / gPrime;
            t = Clamp01(t); // if t gets clampped, then the closest point is one of the curve extremes
        }

        Vector2 closestPoint = BezierCurveFormulas.Cubic(P0, P1, P2, P3, t);
        float distance = Vector2.Distance(Q, closestPoint);

        return new DistanceToCurveResult(t, closestPoint, distance);
    }
    #endregion


    #region distance from curve to curve
    public static Closest2PointsResult ClosestPosition(ICubicSegment s1, ICubicSegment s2, int minSamples = DEFAULT_MIN_SAMPLES)
    {
        /*
            1. Compare distance of 20 samples from s1 each with another 20 samples of s2 (400 total iterations)
            2. Pick best (t,s)
            3. 5 Newton 2D iterations
            4. Clamp t & s to [0,1]
        */
        float bestDistance = float.MaxValue;
        float s = .5f;
        float t = .5f;
        float bestS = s;
        float bestT = t;

        for (int i = 0; i <= minSamples; i++)
        {
            s = i / (float)minSamples;
            Vector2 p1 = BezierCurveFormulas.Cubic(s1, s);
            
            for (int j = 0; j <= minSamples; j++)
            {
                t = j / (float)minSamples;
                Vector2 p2 = BezierCurveFormulas.Cubic(s2, t);

                float currentDistance = Vector2.DistanceSquared(p1, p2);

                if (currentDistance < bestDistance)
                {
                    bestDistance = currentDistance;
                    bestS = s;
                    bestT = t;
                }
            }
        }

        // Refine with 2D Newton-Raphson
        for (int i = 0; i < 5; i++)
        {
            // points along curves
            Vector2 curve1Point = BezierCurveFormulas.Cubic(s1, s);
            Vector2 curve2Point = BezierCurveFormulas.Cubic(s2, t);

            // curves tangents
            Vector2 c1Tangent = BezierCurveFormulas.CubicPrime(s1, s);
            Vector2 c2Tangent = BezierCurveFormulas.CubicPrime(s2, t);

            // curves second derivative
            Vector2 c1ddp = BezierCurveFormulas.CubicSecond(s1, s);
            Vector2 c2ddp = BezierCurveFormulas.CubicSecond(s2, t);

            Vector2 direction = curve1Point - curve2Point;

            // Gradient:
            float gradient1 = Vector2.Dot(direction, c1Tangent);
            float gradient2 = -1 * Vector2.Dot(direction, c2Tangent);

            // Jacobian 2x2 matrix
            float J11 = Vector2.Dot(c1Tangent, c1Tangent) + Vector2.Dot(direction, c1ddp);
            float J12 = -Vector2.Dot(c1Tangent, c2Tangent);
            float J21 = J12;
            float J22 = Vector2.Dot(c2Tangent, c2Tangent) + Vector2.Dot(direction, c2ddp);

            // Solve the 2x2 system
            float det = J11 * J22 - J12 * J21;

            if (float.Abs(det) < 1e-10) break;      // avoids division by 0

            float ds = ( J22 * gradient1 - J12 * gradient2) / det;
            float dt = (-J21 * gradient1 + J11 * gradient2) / det;
            
            s -= ds;
            t -= dt;
            
            s = Clamp01(s);
            t = Clamp01(t);

            if (Math.Abs(ds) < 1e-8 && Math.Abs(dt) < 1e-8) break;  // It converged, so we stop
        }

        Vector2 closestS1Point = BezierCurveFormulas.Cubic(s1, s);
        Vector2 closestS2Point = BezierCurveFormulas.Cubic(s2, t);

        return new Closest2PointsResult
        {
            S = s,
            T = t,
            B1Point = closestS1Point,
            B2Point = closestS2Point,
            Distance = Vector2.Distance(closestS1Point, closestS2Point)
        };
    #endregion
    }
}
