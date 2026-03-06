using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives.Paths;

public class BezierSubpath
{
    public List<IBezierSubpathNode> Nodes;

    public CurvePosition ClosestPosition(Vector2 p)
    {
        float minDistance = float.MaxValue;
        var result = new CurvePosition
        {
            T = 0,
            Point = new Vector2()
        };

        foreach (var node in Nodes)
        {
            var (distance, nodeClosestPos) = node.ClosestPosition(p);
            if (distance < minDistance)
            {
                minDistance = distance;
                result = nodeClosestPos;
            }
        }
        return result;
    }


    public (float, CurvePosition, CurvePosition) ClosestPositions(BezierSubpath subpath)
    {
        float minDistance = float.MaxValue;
        CurvePosition minSelfPos = new CurvePosition();
        CurvePosition minSubpathPos = new CurvePosition();

        foreach (var selfNode in Nodes)
        {
            foreach (var subpathNode in subpath.Nodes)
            {
                var (currentMinimum, currentSelfBestPos, currentSubpathBestPos) = selfNode.ClosestPositions(subpathNode);
                if (currentMinimum < minDistance)
                {
                    minDistance = currentMinimum;
                    minSelfPos = currentSelfBestPos;
                    minSubpathPos = currentSubpathBestPos;
                }
            }
        }

        return (minDistance, minSelfPos, minSubpathPos);
    }
}
