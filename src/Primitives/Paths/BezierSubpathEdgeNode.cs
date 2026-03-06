using GraphicsEngine.Primitives.Interfaces;
using GraphicsEngine.Primitives.Math;

namespace GraphicsEngine.Primitives.Paths;

public class BezierSubpathEdgeNode : IBezierSubpathNode
{
    public ISegment4 Segment { get; private set; }
    public CubicBezierCurve Curve { get; private set; }

    public float Length => throw new NotImplementedException();

    public float Area => throw new NotImplementedException();

    public BoundingBox AxisAlignedBounds => throw new NotImplementedException();

    public float Orientation => throw new NotImplementedException();

    public BezierSubpathEdgeNode(ISegment4 segment)
    {
        Segment = segment;
        Curve = new CubicBezierCurve(segment);
    }

    public CurvePosition Sample(float t)
        => Curve.Sample(t);

    public CurvePosition SampleDerivative(float t)
        => Curve.SamplePrime(t);

    public CurvePosition SampleSecondDerivative(float t)
        => Curve.SampleSecond(t);

    public float CalcGradient(Vector2 direction, float t)
        => Vector2.Dot(direction, Curve.SamplePrime(t).Point);

    public float CalcGradientPrime(Vector2 direction, float t)
    {
        Vector2 primePoint = Curve.SamplePrime(t).Point;
        return Vector2.Dot(primePoint, primePoint) + Vector2.Dot(direction, Curve.SampleSecond(t).Point);
    }

    public IBezierSubpathNode Copy(SubpathCopyDirection direction)
    {
        ISegment4 newSegment = direction == SubpathCopyDirection.FORWARD
            ? new CubicBezierSegment(Segment.P0, Segment.P1, Segment.P2, Segment.P3)
            : Segment.Reversed;
        return new BezierSubpathEdgeNode(newSegment);
    }

    public (float, CurvePosition) AproximateClosestPosition(Vector2 p, int samples = 20)
    {
        float tBest = 0f;
        float minDistanceSquared = float.MaxValue;

        // sample lightly, looking for good a candidate sector for then refining with Newton-Raphson
        foreach (var curvePos in Curve.Sample(samples))
        {
            // no need for actual distance, since this is just an aproximation
            float distanceSquared = Vector2.DistanceSquared(curvePos.Point, p);

            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                tBest = curvePos.T;
            }
        }

        return new(minDistanceSquared, Curve.Sample(tBest));
    }

    public (float, CurvePosition) ClosestPosition(Vector2 p)
    {
        // get an aproximated closest position
        var (bestDistance, bestCurvePos) = AproximateClosestPosition(p);

        // Refine aproximation with Newton-Raphson
        float t = bestCurvePos.T;
        for (int i = 0; i < 5; i++)
        {
            CurvePosition curvePos = Curve.Sample(t);
            CurvePosition primePos = Curve.SamplePrime(t);
            CurvePosition secondPos = Curve.SampleSecond(t);

            Vector2 direction = curvePos.Point - p;
            float gradient = CalcGradient(direction, t);
            float gradientPrime = CalcGradientPrime(direction, t);

            if (MathF.Abs(gradientPrime) < 1e-6f) break; // avoids dividing by cero

            t -= gradient / gradientPrime;
            t = t.Clamp01(); // if t gets clampped, then the closest point is one of the curve extremes
        }

        CurvePosition result = Curve.Sample(t);

        return new(Vector2.Distance(result.Point, p), result);
    }

    public (float, CurvePosition, CurvePosition) ClosestPositions(IBezierSubpathNode subpath)
    {
        // First, sample both curves and find the aproximated closest positions between them
        int samples = 20;

        float minDistance = float.MaxValue;
        CurvePosition bestAproxSelf = new CurvePosition();
        CurvePosition bestAproxSubpath = new CurvePosition();

        foreach (var sampledPos in Curve.Sample(samples))
        {
            var (distance, aproxClosestPos) = subpath.AproximateClosestPosition(sampledPos.Point, samples);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestAproxSelf = sampledPos;
                bestAproxSubpath = aproxClosestPos;
            }
        }

        // Refine with 2D Newton-Raphson
        float s = bestAproxSelf.T;
        float t = bestAproxSubpath.T;
        Vector2 selfPos = new Vector2();
        Vector2 subpathPos = new Vector2();

        for (int i = 0; i < 5; i++)
        {
            // points along curves
            selfPos = Sample(s).Point;
            subpathPos = subpath.Sample(t).Point;

            // curves tangents
            Vector2 selfTangent = SampleDerivative(s).Point;
            Vector2 subpathTangent = subpath.SampleDerivative(t).Point;

            // curves second derivative
            Vector2 c1ddp = SampleSecondDerivative(s).Point;
            Vector2 c2ddp = subpath.SampleSecondDerivative(t).Point;

            Vector2 direction = selfPos - subpathPos;

            // Jacobian 2x2 matrix
            float J11 = Vector2.Dot(selfTangent, selfTangent) + Vector2.Dot(direction, c1ddp);
            float J12 = -Vector2.Dot(selfTangent, subpathTangent);
            float J21 = J12;
            float J22 = Vector2.Dot(subpathTangent, subpathTangent) + Vector2.Dot(direction, c2ddp);

            // Solve the 2x2 system
            float det = J11 * J22 - J12 * J21;

            if (float.Abs(det) < 1e-10) break;      // avoids division by 0

            // Gradient:
            float gradient1 = CalcGradient(direction, s);
            float gradient2 = -1 * subpath.CalcGradient(direction, t);

            float ds = (J22 * gradient1 - J12 * gradient2) / det;
            float dt = (-J21 * gradient1 + J11 * gradient2) / det;

            s -= ds;
            t -= dt;

            s = s.Clamp01();
            t = t.Clamp01();

            if (MathF.Abs(ds) < 1e-8 && MathF.Abs(dt) < 1e-8) break;  // It converged, so we stop
        }

        return new(
            Vector2.Distance(selfPos, subpathPos),
            new CurvePosition
            {
                T = s,
                Point = selfPos
            },
            new CurvePosition
            {
                T = t,
                Point = subpathPos
            }
        );
    }

    public CurvePosition[] Intersect(ISegment segment)
    {
        throw new NotImplementedException();
    }

    public float PointRelativePosition(Vector2 p)
    {
        throw new NotImplementedException();
    }
}
