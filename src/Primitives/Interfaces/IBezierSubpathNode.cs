using GraphicsEngine.Primitives.Paths;

namespace GraphicsEngine.Primitives.Interfaces;


public interface IBezierSubpathNode
{
    float Length { get; }
    float Area { get; }
    BoundingBox AxisAlignedBounds { get; }
    float Orientation { get; }
    /*
        An operation to calculate and return a list of all positions that correspond to
        the Intersection between a subpath and a straight line segment (made of just
        2 points)
        An operation to determine the Orientation of a given subpath, which can be Clockwise or Counterclockwise.
        Also to calculate its Length, its Area and its Axis-aligned Bounding Box
        An operation to determine whether an arbitrary point P is Inside, Outside or Along a subpath.
     */
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
