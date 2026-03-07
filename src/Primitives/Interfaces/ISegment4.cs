namespace GraphicsEngine.Primitives.Interfaces;

public interface ISegment4 : ISegment
{
    Vector2 P2 { get; }
    Vector2 P3 { get; }
    ISegment S1 { get; }
    ISegment S2 { get; }
    ISegment S3 { get; }
    ISegment[] Segments { get; }
    ISegment4 Reversed { get; }
    void Reverse();
}
