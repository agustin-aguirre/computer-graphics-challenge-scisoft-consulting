using NUnit.Framework;

namespace GraphicsEngine.Primitives.Tests;


[TestFixture]
public class Point2DTests
{
    [Test]
    public void Sum_WhenCalled_ReturnsSumOfVectors()
    {
        // Arrange
        Point2D a = new Point2D(5f, 3f);
        Point2D b = new Point2D(2f, 1f);

        // Act
        Point2D r = a + b;

        // Assert
        Assert.AreEqual(r, new Point2D(7f, 4f));
    }

    [Test]
    public void MultiplicationWithScalar_WhenCalled_ReturnsVectorMultipliedByScalar()
    {
        // Arrange
        Point2D a = new Point2D(5f, 3f);
        float k = 3f;

        // Act
        Point2D r = a * k;

        // Assert
        Assert.AreEqual(r, new Point2D(15f, 9f));
    }
}