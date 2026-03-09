using GraphicsEngine.Primitives.Results;

namespace GraphicsEngine.Primitives.Interfaces;

public interface IBezierSubpathNode
{
    float Length { get; }
    float Area { get; }
    BoundingBox Bounds { get; }
    CurveOrientation Orientation { get; }
    IBezierSubpathNode Copy();
    CurvePosition Sample(float t);
    CurvePosition SampleDerivative(float t);
    CurvePosition SampleSecondDerivative(float t);
    (float, CurvePosition) AproximateClosestPosition(Vector2 p, int samples);
    (float, CurvePosition) ClosestPosition(Vector2 p);
    (float, CurvePosition, CurvePosition) ClosestPositions(IBezierSubpathNode subpath);
    float CalcGradient(Vector2 direction, float t);
    float CalcGradientPrime(Vector2 direction, float t);
    IntersectionResult[] Intersect(ISegment segment);
    PointInCurveResult BoundaryPosition(Vector2 p);
}
