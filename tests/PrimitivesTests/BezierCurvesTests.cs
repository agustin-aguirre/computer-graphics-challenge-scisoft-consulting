using NUnit.Framework;

namespace GraphicsEngine.Primitives.Tests;

[TestFixture]
public class BezierCurvesTests
{
    /*const float Tolerance = 1e-4f;

    readonly Vector2 p0 = new Vector2(2.65f, -1.5f);
    readonly Vector2 p1 = new Vector2(8.2f, -1.18f);
    readonly Vector2 p2 = new Vector2(6.46f, 6.4f);
    readonly Vector2 p3 = new Vector2(1.58f, 4.21f);

    [Test]
    public void CubicBezierSegment_WhenEvaluated_CalculatesTheCurveCorrectly()
    {
        // Arrange
        CubicBezierCurve b = new CubicBezierCurve(new CubicBezierSegment(p0, p1, p2, p3));

        // Act
        Vector2[] results = runCurveEvaluation(b);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(results[0], Is.EqualTo(p0).Within(Tolerance));
            Assert.AreEqual(results[0], p0);
            Assert.AreEqual(results[1], new Vector2(5.51f, -0.16f));
            Assert.AreEqual(results[2], new Vector2(6.02f, 2.29f));
            Assert.AreEqual(results[3], new Vector2(4.58f, 4.28f));
            Assert.AreEqual(results[4], p3);
        });
    }

    private Vector2[] runCurveEvaluation(ICubicBezierCurve curve)
    {
        var results = new Vector2[5]
        {
            curve.Evaluate(0f),
            curve.Evaluate(.25f),
            curve.Evaluate(.5f),
            curve.Evaluate(.75f),
            curve.Evaluate(1f)
        };
        return results;
    }*/
}
