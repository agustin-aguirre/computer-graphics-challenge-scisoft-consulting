using NUnit.Framework;
using System;
using static GraphicsEngine.Primitives.Utils.FloatUtils;

namespace GraphicsEngine.Primitives.Tests;


[TestFixture]
public class Vector2Tests
{
    [Test]
    public void Sum_WhenCalled_ReturnsSumOfVectors()
    {
        // Arrange
        Vector2 a = new Vector2(5f, 3f);
        Vector2 b = new Vector2(2f, 1f);

        // Act
        Vector2 r = a + b;

        // Assert
        Assert.AreEqual(r, new Vector2(7f, 4f));
    }

    [Test]
    public void MultiplicationWithScalar_WhenCalled_ReturnsVectorMultipliedByScalar()
    {
        // Arrange
        Vector2 a = new Vector2(5f, 3f);
        float k = 3f;

        // Act
        Vector2 r = a * k;

        // Assert
        Assert.AreEqual(r, new Vector2(15f, 9f));
    }

    [Test]
    public void Magnitude_WhenCalled_ReturnsCorrectValue()
    {
        // Arrange
        Vector2 v1 = new Vector2(0f, 0f);
        Vector2 v2 = new Vector2(1f, 0f);
        Vector2 v3 = new Vector2(0f, 1f);
        Vector2 v4 = new Vector2(-3f, 4f);
        Vector2 v5 = new Vector2(1f, 1f);

        // Act
        float r1 = v1.Magnitude;
        float r2 = v2.Magnitude;
        float r3 = v3.Magnitude;
        float r4 = v4.Magnitude;
        float r5 = v5.Magnitude;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(0f, r1);
            Assert.AreEqual(1f, r2);
            Assert.AreEqual(1f, r3);
            Assert.AreEqual(RoundDown(MathF.Sqrt(2f), 2), RoundDown(r5, 2));
        });
    }

    [Test]
    public void DotProduct_WhenCalled_ReturnsCorrectValue()
    {
        // Arrange
        Vector2 v1 = new Vector2(1f, 0f);
        Vector2 v2 = new Vector2(0f, 1f);

        // Act
        float r1 = Vector2.Dot(v1, v1);
        float r2 = Vector2.Dot(v1, v2);
        float r3 = Vector2.Dot(v1, -1 * v1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(1f, r1);
            Assert.AreEqual(0f, r2);
            Assert.AreEqual(-1f, r3);
        });
    }

    [Test]
    public void Angle_WhenCalled_ReturnsCorrectValue()
    {
        // Arrange
        Vector2 v1 = new Vector2(1f, 0f);
        Vector2 v2 = new Vector2(0f, 1f);
        Vector2 v3 = new Vector2(-1f, 0f);
        Vector2 v4 = new Vector2(1f, 1f);

        float half_pi = RoundDown(MathF.PI * .5f, 2);
        float quarter_pi = RoundDown(MathF.PI * .25f, 2);
        float pi = RoundDown(MathF.PI, 2);
        float tau = RoundDown(MathF.Tau, 2);

        // Act
        float r1 = Vector2.Angle(v1, v2);
        float r2 = Vector2.Angle(v1, v3);
        float r3 = Vector2.Angle(v1, v1);
        float r4 = Vector2.Angle(v1, v4);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(half_pi, RoundDown(r1, 2));
            Assert.AreEqual(pi, RoundDown(r2, 2));
            Assert.AreEqual(0f, RoundDown(r3, 2));
            Assert.AreEqual(quarter_pi, RoundDown(r4, 2));
        });
    }
}