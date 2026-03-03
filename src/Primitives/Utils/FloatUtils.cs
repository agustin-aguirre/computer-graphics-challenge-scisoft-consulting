namespace GraphicsEngine.Primitives.Utils;

public static class FloatUtils
{
    public static float Clamp(float value, float min, float max)
    {
        return MathF.Min(MathF.Max(value, min), max);
    }

    public static float Clamp01(float value)
    {
        return Clamp(value, 0f, 1f);
    }

    public static float Square(float value)
    {
        return MathF.Pow(value, 2f);
    }
}
