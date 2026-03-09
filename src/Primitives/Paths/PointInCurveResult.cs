namespace GraphicsEngine.Primitives.Paths;

/// <summary>
/// Representation of the relative position of a point in regard of a Curve, where Outside = 0, Inside = 1 and Along = 2.
/// </summary>
public enum PointInCurveResult
{
    Outside = 0,
    Inside = 1,
    Along = 2
}
