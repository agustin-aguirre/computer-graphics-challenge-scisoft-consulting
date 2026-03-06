namespace GraphicsEngine.Primitives.Math;

public static class FloatExtensions
{
    public static float Clamp(this float v, float min, float max)
    {
        return MathF.Min(MathF.Max(v, min), max);
    }

    public static float Clamp01(this float v)
    {
        return v.Clamp(0, 1);
    }
}