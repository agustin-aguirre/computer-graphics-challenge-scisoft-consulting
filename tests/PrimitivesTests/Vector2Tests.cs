using NUnit.Framework;

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
}