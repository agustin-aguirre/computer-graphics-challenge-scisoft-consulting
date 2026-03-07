using GraphicsEngine;
using GraphicsEngine.Primitives;
using GraphicsEngine.Primitives.Interfaces;
using NUnit.Framework;

namespace PrimitivesTests;

[TestFixture]
public class BezierLengthTests
{
    [Test]
    public void StraightLine_ShouldEqualDistance()
    {
        var p0 = new Vector2(0, 0);
        var p1 = new Vector2(3, 0);
        var p2 = new Vector2(7, 0);
        var p3 = new Vector2(10, 0);
        
        ISegment4 s = new CubicBezierSegment(p0, p1, p2, p3);

        CubicBezierCurve curve = new CubicBezierCurve(s);

        float length = curve.Length;

        Assert.That(length, Is.EqualTo(10).Within(1e-6));
    }

    [Test]
    public void SamePoints_ShouldBeZero()
    {
        var p = new Vector2(5, 5);

        ISegment4 s = new CubicBezierSegment(p, p, p, p);
        
        CubicBezierCurve curve = new CubicBezierCurve(s);
        
        float length = curve.Length;

        Assert.That(length, Is.EqualTo(0).Within(1e-9));
    }

    [Test]
    public void ReversedCurve_ShouldHaveSameLength()
    {
        var p0 = new Vector2(0, 0);
        var p1 = new Vector2(0, 10);
        var p2 = new Vector2(10, 10);
        var p3 = new Vector2(10, 0);

        ISegment4 s = new CubicBezierSegment(p0, p1, p2, p3);

        CubicBezierCurve forwardCurve = new CubicBezierCurve(s);
        CubicBezierCurve reverseCurve = new CubicBezierCurve(s.Reversed);

        float forward = forwardCurve.Length;
        float reverse = reverseCurve.Length;

        Assert.That(forward, Is.EqualTo(reverse).Within(1e-9));
    }

    [Test]
    public void CurveLength_ShouldBeGreaterThanChord()
    {
        var p0 = new Vector2(0, 0);
        var p1 = new Vector2(0, 10);
        var p2 = new Vector2(10, 10);
        var p3 = new Vector2(10, 0);

        ISegment4 s = new CubicBezierSegment(p0, p1, p2, p3);

        CubicBezierCurve c = new CubicBezierCurve(s);

        float length = c.Length;

        double chord = Vector2.Distance(p0, p3);

        Assert.That(length, Is.GreaterThanOrEqualTo(chord));
    }

    [Test]
    public void Translation_ShouldNotChangeLength()
    {
        var offset = new Vector2(100, 200);

        var p0 = new Vector2(0, 0);
        var p1 = new Vector2(2, 3);
        var p2 = new Vector2(4, 5);
        var p3 = new Vector2(6, 0);

        ISegment4 s = new CubicBezierSegment(p0, p1, p2, p3);
        
        float length1 = new CubicBezierCurve(s).Length;

        p0 = p0 + offset;
        p1 = p1 + offset;
        p2 = p2 + offset;
        p3 = p3 + offset;

        s = new CubicBezierSegment(p0, p1, p2, p3);
        float length2 = new CubicBezierCurve(s).Length;

        Assert.That(length1, Is.EqualTo(length2).Within(1e-9));
    }

    [Test]
    public void Scaling_ShouldScaleLength()
    {
        var p0 = new Vector2(0, 0);
        var p1 = new Vector2(1, 3);
        var p2 = new Vector2(4, 5);
        var p3 = new Vector2(6, 0);

        ISegment4 s1 = new CubicBezierSegment(p0, p1, p2, p3);

        float length = new CubicBezierCurve(s1).Length;

        float scale = 3f;

        ISegment4 s2 = new CubicBezierSegment(p0 * scale, p1 * scale, p2 * scale, p3 * scale);

        float scaled = new CubicBezierCurve(s2).Length;

        Assert.That(scaled, Is.EqualTo(length * scale).Within(1e-5));
    }
}