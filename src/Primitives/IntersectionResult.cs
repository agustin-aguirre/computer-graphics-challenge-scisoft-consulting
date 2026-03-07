namespace GraphicsEngine.Primitives;


/// <summary>
/// Representation of all the intersecting points of a pair of bezier segments
/// Points contains all the intersected points
/// Positions contains the position in S1 and S2 at wich the intersection was detected.
/// Both Points and Positions must be of equal length.
/// No Points or Positions means, the segments are parallel
/// </summary>
public struct IntersectionResult
{
    public Vector2[] Points;
    public (float, float)[] Positions;
}
