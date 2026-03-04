using NUnit.Framework;
using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives.Tests;

[TestFixture]
public class BezierCurvesTests
{
    readonly Vector2 p0 = new Vector2(2.65f, -1.5f);
    readonly Vector2 p1 = new Vector2(8.2f, -1.18f);
    readonly Vector2 p2 = new Vector2(6.46f, 6.4f);
    readonly Vector2 p3 = new Vector2(1.58f, 4.21f);


    [Test]
    public void LinearBezierSegment_WhenEvaluated_CalculatesTheCurveCorrectly()
    {
        // Arrange
        IBezierCurve b = new LinearBezier(p0, p1);

        // Act
        Vector2[] results = runCurveEvaluation(b);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(results[0].RoundedDown(2), p0);
            Assert.AreEqual(results[1].RoundedDown(2), new Vector2(4.03f, -1.42f));
            Assert.AreEqual(results[2].RoundedDown(2), new Vector2(5.42f, -1.33f));
            Assert.AreEqual(results[3].RoundedDown(2), new Vector2(6.81f, -1.26f));
            // Not rounding last one since it results in a very tiny floating point rounding error
            // and there's no need for rounding it since it should result in p1 anyways
            Assert.AreEqual(results[4], p1);
        });
    }


    [Test]
    public void QuadraticBezierSegment_WhenEvaluated_CalculatesTheCurveCorrectly()
    {
        // Arrange
        IBezierCurve b = new QuadraticBezier(p0, p1, p2);

        // Act
        Vector2[] results = runCurveEvaluation(b);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(results[0].RoundedDown(2), p0);
            Assert.AreEqual(results[1].RoundedDown(2), new Vector2(4.96f, -0.88f));
            Assert.AreEqual(results[2].RoundedDown(2), new Vector2(6.37f, 0.63f));
            Assert.AreEqual(results[3].RoundedDown(2), new Vector2(6.87f, 3.06f));
            Assert.AreEqual(results[4].RoundedDown(2), p2);
        });
    }

    [Test]
    public void CubicBezierSegment_WhenEvaluated_CalculatesTheCurveCorrectly()
    {
        // Arrange
        IBezierCurve b = new CubicBezier(p0, p1, p2, p3);

        // Act
        Vector2[] results = runCurveEvaluation(b);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(results[0].RoundedDown(2), p0);
            Assert.AreEqual(results[1].RoundedDown(2), new Vector2(5.51f, -0.16f));
            Assert.AreEqual(results[2].RoundedDown(2), new Vector2(6.02f, 2.29f));
            Assert.AreEqual(results[3].RoundedDown(2), new Vector2(4.58f, 4.28f));
            Assert.AreEqual(results[4].RoundedDown(2), p3);
        });
    }

    private Vector2[] runCurveEvaluation(IBezierCurve curve)
    {
        var results = new Vector2[5]
        {
            curve.GetCurvePointAt(0f),
            curve.GetCurvePointAt(.25f),
            curve.GetCurvePointAt(.5f),
            curve.GetCurvePointAt(.75f),
            curve.GetCurvePointAt(1f)
        };
        return results;
    }
}
