using GraphicsEngine.Primitives.Interfaces;
using NUnit.Framework;
using System;

namespace GraphicsEngine.Primitives.Tests;


[TestFixture]
public class BezierDistanceTests
{
    /*const float Tolerance = 1e-4f;

    [Test]
    public void Point_Along_Curve_Must_Be_At_Cero_Distance()
    {
        Vector2 P0 = new(0, 0);
        Vector2 P1 = new(1, 3);
        Vector2 P2 = new(3, 3);
        Vector2 P3 = new(4, 0);

        ICubicBezierCurve B = new CubicBezier(P0, P1, P2, P3);

        float t = 0.5f;
        Vector2 Q = B.Evaluate(t);
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




    // ==============================
    // 1️⃣ Curvas paralelas conocidas
    // ==============================
    [Test]
    public void ParallelLines_KnownDistance()
    {
        var b1 = new CubicBezier(new Vector2(0, 0), new Vector2(3, 0), new Vector2(7, 0), new Vector2(10, 0));
        var b2 = new CubicBezier(new Vector2(0, 5), new Vector2(3, 5), new Vector2(7, 5), new Vector2(10, 5));


        var result = BezierOps.ClosestPosition(b1, b2);

        Assert.That(result.Distance, Is.EqualTo(5).Within(Tolerance));
    }

    // ==============================
    // 2️⃣ Intersección exacta
    // ==============================
    [Test]
    public void IntersectingCurves_DistanceZero()
    {
        var b1 = new CubicBezier(new Vector2(0, 0), new Vector2(3, 5), new Vector2(7, -5), new Vector2(10, 0));
        var b2 = new CubicBezier(new Vector2(0, 0),new Vector2(3, -5),new Vector2(7, 5),new Vector2(10, 0));
        var result = BezierOps.ClosestPosition(b1, b2);
        Assert.That(result.Distance, Is.EqualTo(0).Within(Tolerance));
    }

    // ==============================
    // 3️⃣ Mínimo en extremo
    // ==============================
    [Test]
    public void EndpointMinimum()
    {
        var b1 = new CubicBezier(new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0));
        var b2 = new CubicBezier(new Vector2(10, 10), new Vector2(11, 10), new Vector2(12, 10), new Vector2(13, 10));

        var result = BezierOps.ClosestPosition(b1, b2);

        Assert.That(result.S, Is.EqualTo(1).Within(Tolerance));
        Assert.That(result.T, Is.EqualTo(0).Within(Tolerance));
    }

    // ==============================
    // 4️⃣ Simetría
    // ==============================
    [Test]
    public void SymmetryTest()
    {
        var b1 = RandomCurve();
        var b2 = RandomCurve();

        var r1 = BezierOps.ClosestPosition(b1, b2);
        var r2 = BezierOps.ClosestPosition(b2, b1);

        Assert.That(r1.Distance, Is.EqualTo(r2.Distance).Within(Tolerance));
    }

    // ==============================
    // 5️⃣ Condición de ortogonalidad
    // ==============================
    [Test]
    public void OrthogonalityCondition()
    {
        var b1 = RandomCurve();
        var b2 = RandomCurve();

        var result = BezierOps.ClosestPosition(b1, b2);

        var d = result.B1Point - result.B2Point;

        var d1 = EvaluateDerivative(b1, result.S);
        var d2 = EvaluateDerivative(b2, result.T);

        Assert.That(Vector2.Dot(d, d1), Is.EqualTo(0).Within(Tolerance));
        Assert.That(Vector2.Dot(d, d2), Is.EqualTo(0).Within(Tolerance));
    }

    // ==============================
    // 6️⃣ Stress test aleatorio
    // ==============================
    [Test]
    public void RandomStressTest()
    {
        for (int i = 0; i < 100; i++)
        {
            var b1 = RandomCurve();
            var b2 = RandomCurve();

            var result = BezierOps.ClosestPosition(b1, b2);

            Assert.That(result.Distance, Is.GreaterThanOrEqualTo(0));
            Assert.That(result.S, Is.InRange(0, 1));
            Assert.That(result.T, Is.InRange(0, 1));
        }
    }

    // ==============================
    // Helpers
    // ==============================

    private static CubicBezier RandomCurve()
    {
        var rand = new Random(Guid.NewGuid().GetHashCode());

        return new CubicBezier
        (
            RandomVec(rand),
            RandomVec(rand),
            RandomVec(rand),
            RandomVec(rand)
        );
    }

    private static Vector2 RandomVec(Random r)
    {
        return new Vector2(
            (float)(r.NextDouble() * 20 - 10),
            (float)(r.NextDouble() * 20 - 10));
    }

    // Derivada explícita de cúbica (por si tu implementación no la expone)
    private static Vector2 EvaluateDerivative(CubicBezier b, double t)
    {
        double u = 1 - t;

        return
            3f * (float)(u * u) * (b.P1 - b.P0) +
            6f * (float)(u * t) * (b.P2 - b.P1) +
            3f * (float)(t * t) * (b.P3 - b.P2);
    }*/
}
