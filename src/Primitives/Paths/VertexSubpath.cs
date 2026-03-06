using GraphicsEngine.Primitives.Interfaces;
using GraphicsEngine.Primitives.Utils;

namespace GraphicsEngine.Primitives.Paths;

public class VertexSubpath : IBezierSubpath
{
    Vector2 vertex;
    public int Size => 1;
    public IEnumerable<Vector2> Points => [vertex];
    public ISegment4 Segment4 => new CubicBezierSegment(vertex, vertex, vertex, vertex);
    public float SegmentLength => 0;
    public float CurveLength => 0;

    public IBezierSubpath Reversed => Clone();

    public VertexSubpath(Vector2 vertex)
    {
        this.vertex = vertex;
    }

    public ISubpathPosition SampleAt(float t)
    {
        return new SubpathPosition()
        {
            T = t,
            Point = vertex
        };
    }

    public IEnumerable<ISubpathPosition> TakeSamples(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float t = i / amount;
            yield return SampleAt(t);
        }
    }


    public ISubpathPosition ClosestPosition(Vector2 point)
    {
        return new SubpathPosition
        {
            T = 0,
            Point = vertex
        };
    }

    public IClosestPositionsResult ClosestPositions(IBezierSubpath subpath)
    {
        var closestPositionResult = BezierOps.ClosestPosition(subpath.Segment4, vertex, 20);
        
        return new Closest2PointsResult
        {
            Distance = closestPositionResult.Distance,
            Subpath1 = new SubpathPosition
            {
                Point = vertex,
                T = 0
            },
            Subpath2 = new SubpathPosition
            {
                Point = closestPositionResult.Point,
                T = closestPositionResult.T
            }
        };
    }

    public void Reverse()
    {
        // Empty on purpose
    }

    public IBezierSubpath Clone()
    {
        return new VertexSubpath(vertex);
    }
}
