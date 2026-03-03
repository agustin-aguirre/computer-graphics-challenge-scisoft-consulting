namespace GraphicsEngine.Primitives.Interfaces;

public interface ICubicSegment : IQuadraticSegment
{
    Vector2 P3 { get; }
}
