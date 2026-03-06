using GraphicsEngine.Primitives.Paths;

namespace GraphicsEngine.Primitives.Interfaces;


public interface IBezierSubpathNode
{
    float Length { get; }
    float Area { get; }
    BoundingBox AxisAlignedBounds { get; }
    float Orientation { get; }
    IBezierSubpathNode Copy(SubpathCopyDirection direction);
    CurvePosition Sample(float t);
    CurvePosition SampleDerivative(float t);
    CurvePosition SampleSecondDerivative(float t);
    (float, CurvePosition) AproximateClosestPosition(Vector2 p, int samples);
    (float, CurvePosition) ClosestPosition(Vector2 p);
    (float, CurvePosition, CurvePosition) ClosestPositions(IBezierSubpathNode subpath);
    float CalcGradient(Vector2 direction, float t);
    float CalcGradientPrime(Vector2 direction, float t);
    CurvePosition[] Intersect(ISegment segment);
    float PointRelativePosition(Vector2 p);
}
