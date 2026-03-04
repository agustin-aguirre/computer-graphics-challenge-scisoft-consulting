namespace GraphicsEngine.Primitives.Interfaces;

public interface IDistanceToCurveResult
{
    float T { get; }
    Vector2 Point { get; }
    float Distance { get; }
}
