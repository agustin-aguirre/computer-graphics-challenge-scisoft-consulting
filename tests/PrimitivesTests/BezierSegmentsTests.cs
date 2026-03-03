using NUnit.Framework;
using GraphicsEngine.Primitives.Interfaces;

namespace GraphicsEngine.Primitives.Tests;

[TestFixture]
public class BezierSegmentsTests
{
    readonly Point2D p0 = new Point2D(2.65f, -1.5f);
    readonly Point2D p1 = new Point2D(8.2f, -1.18f);
    readonly Point2D p2 = new Point2D(6.46f, 6.4f);
    readonly Point2D p3 = new Point2D(1.58f, 4.21f);


    [Test]
    public void LinearBezierSegment_WhenEvaluated_CalculatesTheCurveCorrectly()
    {
        // Arrange
        IBezierCurve b = new LinearBezierSegment(p0, p1);

        // Act
        Point2D[] results = runCurveEvaluation(b);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(results[0].RoundedDown(2), p0);
            Assert.AreEqual(results[1].RoundedDown(2), new Point2D(4.03f, -1.42f));
            Assert.AreEqual(results[2].RoundedDown(2), new Point2D(5.42f, -1.33f));
            Assert.AreEqual(results[3].RoundedDown(2), new Point2D(6.81f, -1.26f));
            // Not rounding last one since it results in a very tiny floating point rounding error
            // and there's no need for rounding it since it should result in p1 anyways
            Assert.AreEqual(results[4], p1);
        });
    }


    [Test]
    public void QuadraticBezierSegment_WhenEvaluated_CalculatesTheCurveCorrectly()
    {
        // Arrange
        IBezierCurve b = new QuadraticBezierSegment(p0, p1, p2);

        // Act
        Point2D[] results = runCurveEvaluation(b);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(results[0].RoundedDown(2), p0);
            Assert.AreEqual(results[1].RoundedDown(2), new Point2D(4.96f, -0.88f));
            Assert.AreEqual(results[2].RoundedDown(2), new Point2D(6.37f, 0.63f));
            Assert.AreEqual(results[3].RoundedDown(2), new Point2D(6.87f, 3.06f));
            Assert.AreEqual(results[4].RoundedDown(2), p2);
        });
    }

    [Test]
    public void CubicBezierSegment_WhenEvaluated_CalculatesTheCurveCorrectly()
    {
        // Arrange
        IBezierCurve b = new CubicBezierSegment(p0, p1, p2, p3);

        // Act
        Point2D[] results = runCurveEvaluation(b);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(results[0].RoundedDown(2), p0);
            Assert.AreEqual(results[1].RoundedDown(2), new Point2D(5.51f, -0.16f));
            Assert.AreEqual(results[2].RoundedDown(2), new Point2D(6.02f, 2.29f));
            Assert.AreEqual(results[3].RoundedDown(2), new Point2D(4.58f, 4.28f));
            Assert.AreEqual(results[4].RoundedDown(2), p3);
        });
    }

    private Point2D[] runCurveEvaluation(IBezierCurve curve)
    {
        var results = new Point2D[5]
        {
            curve.Evaluate(0f),
            curve.Evaluate(.25f),
            curve.Evaluate(.5f),
            curve.Evaluate(.75f),
            curve.Evaluate(1f)
        };
        return results;
    }
}
