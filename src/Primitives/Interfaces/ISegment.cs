namespace GraphicsEngine.Primitives.Interfaces;

public interface ISegment
{
    Vector2 P0 { get; }
    Vector2 P1 { get; }
    float Length { get; }
    Vector2[] Points { get; }
    IntersectionResult Intersect(ISegment s);
}