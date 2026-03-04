using NUnit.Framework;
using GraphicsEngine.Primitives.Utils;

namespace GraphicsEngine.Primitives.Tests;


[TestFixture]
public class BezierDistanceTests
{
    const float Tolerance = 1e-4f;

    [Test]
    public void Point_Along_Curve_Must_Be_At_Cero_Distance()
    {
        Vector2 P0 = new(0, 0);
        Vector2 P1 = new(1, 3);
        Vector2 P2 = new(3, 3);
        Vector2 P3 = new(4, 0);

        CubicBezier B = new CubicBezier(P0, P1, P2, P3);

        float t = 0.5f;
        Vector2 Q = B.GetCurvePointAt(t);
        var result = BezierOps.ClosestPosition(B, Q);


        Assert.That(result.Distance, Is.EqualTo(0f).Within(Tolerance));
        Assert.That(result.T, Is.EqualTo(t).Within(0.05f)); // bigger tolerance thanks to Newton
    }

    [Test]
    public void Closest_Point_Is_Initial()
    {
        Vector2 P0 = new(0, 0);
        Vector2 P1 = new(1, 0);
        Vector2 P2 = new(2, 0);
        Vector2 P3 = new(3, 0);

        Vector2 Q = new(-5, 0);

        var result = BezierOps.ClosestPosition(P0, P1, P2, P3, Q);

        Assert.That(result.T, Is.EqualTo(0f).Within(Tolerance));
        Assert.That(result.Point, Is.EqualTo(P0).Using<Vector2>((a, b) => Vector2.Distance(a, b) < Tolerance));
    }

    [Test]
    public void Closest_Point_Is_Final()
    {
        Vector2 P0 = new(0, 0);
        Vector2 P1 = new(1, 0);
        Vector2 P2 = new(2, 0);
        Vector2 P3 = new(3, 0);

        Vector2 Q = new(10, 0);

        var result = BezierOps.ClosestPosition(P0, P1, P2, P3, Q);

        Assert.That(result.T, Is.EqualTo(1f).Within(Tolerance));
        Assert.That(result.Point, Is.EqualTo(P3).Using<Vector2>((a, b) =>
            Vector2.Distance(a, b) < Tolerance));
    }

    [Test]
    public void Lineal_Bezier_Must_Behave_As_Segment()
    {
        // Points form a horizontal line
        Vector2 P0 = new(0, 0);
        Vector2 P1 = new(1, 0);
        Vector2 P2 = new(2, 0);
        Vector2 P3 = new(3, 0);

        Vector2 Q = new(1.5f, 2f);

        var result = BezierOps.ClosestPosition(P0, P1, P2, P3, Q);

        Assert.That(result.Point.Y, Is.EqualTo(0f).Within(Tolerance));
        Assert.That(result.Point.X, Is.EqualTo(1.5f).Within(0.05f));
    }

    [Test]
    public void T_Must_Be_In_Valid_Range()
    {
        Vector2 P0 = new(0, 0);
        Vector2 P1 = new(2, 5);
        Vector2 P2 = new(5, -3);
        Vector2 P3 = new(8, 0);

        Vector2 Q = new(3, 2);

        var result = BezierOps.ClosestPosition(P0, P1, P2, P3, Q);

        Assert.That(result.T, Is.InRange(0f, 1f));
    }
}
