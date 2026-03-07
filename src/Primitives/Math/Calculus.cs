namespace GraphicsEngine.Primitives.Math;

public static class Calculus
{
    /// <summary>
    /// Gauss-Legender Points and Weights for a Polynomial Function of grade 5 or less.
    /// </summary>
    public static (float, float)[] GAUSS_LEGENDER_N5_PWs => [
        (-0.9061798459f, 0.2369268850f),
        (-0.5384693101f, 0.4786286705f),
        (0f, 0.5688888889f),
        (0.5384693101f, 0.4786286705f),
        (0.9061798459f, 0.2369268850f),
    ];
}
