namespace GraphicsEngine.Primitives.Interfaces;

public interface IBezierSubpath : IReversable<IBezierSubpath>, ICloneable<IBezierSubpath>
{
    /// <summary>
    /// Total segment points in the subpath
    /// </summary>
    int Size { get; }
    
    /// <summary>
    /// All the segment points 
    /// </summary>
    IEnumerable<Vector2> Points { get; }
    
    /// <summary>
    /// The sum of all segments
    /// Example: distance(P0, P1) + distance(P1,P2) + ... + distance(P3, P4)
    /// </summary>
    float SegmentLength { get; }

    ISegment4 Segment4 { get; }

    /// <summary>
    /// Evaluates the Curve at position t
    /// </summary>
    /// <param name="t">0 <= t <= 1</param>
    /// <returns></returns>
    ISubpathPosition SampleAt(float t);

    /// <summary>
    /// Takes samples from the whole Curve at equal distances
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    IEnumerable<ISubpathPosition> TakeSamples(int amount);
    ISubpathPosition ClosestPosition(Vector2 point);
    IClosestPositionsResult ClosestPositions(IBezierSubpath subpath);
}