using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives.Paths;

public struct SubpathPosition : ISubpathPosition
{
    public float T { get; init; }
    public Vector2 Point { get; init; }
}
