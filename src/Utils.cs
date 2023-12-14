using System.Numerics;
using Raylib_cs;

public static class Utils
{
    private static float Hue = 0f;

    public static readonly Random Rand = new();

    public static float Clamp(float value, float min, float max)
    {
        if (value > max)
        {
            return max;
        }
        else if (value < min)
        {
            return min;
        }

        return value;
    }

    public static float Clamp01(float value)
    {
        return Clamp(value, 0, 1);
    }

    public static float Pow(float a, float b)
    {
        return (float) Math.Pow(a, b);
    }

    public static Vector2 Project(Vector2 a, Vector2 b)
    {
        return Vector2.Dot(a, b) / b.LengthSquared() * b;
    }

    // https://stackoverflow.com/questions/1335426/is-there-a-built-in-c-net-system-api-for-hsv-to-rgb
    public static Color HSVtoColor(float hue, float saturation, float value)
    {
        int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        float f = hue / 60 - (float) Math.Floor(hue / 60);

        value *= 255;
        int v = Convert.ToInt32(value);
        int p = Convert.ToInt32(value * (1 - saturation));
        int q = Convert.ToInt32(value * (1 - f * saturation));
        int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => new(v, t, p, 255),
            1 => new(q, v, p, 255),
            2 => new(p, v, t, 255),
            3 => new(p, q, v, 255),
            4 => new(t, p, v, 255),
            _ => new(v, p, q, 255),
        };
    }

    public static Color VibrantColor()
    {
        Hue += (float) Rand.NextDouble() * 360;
        Hue %= 360;

        return HSVtoColor(Hue, 0.8f, 1f);
    }

    public static float RandomRange(Random random, float min, float max)
    {
        return (float) random.NextDouble() * (max - min) + min;
    }
}