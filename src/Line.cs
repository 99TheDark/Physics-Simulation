using System.Numerics;
using Raylib_cs;

public class Line : Renderable
{
    public Vector2 A;
    public Vector2 B;

    public Line(Vector2 a, Vector2 b)
    {
        A = a;
        B = b;
    }

    public Line(float ax, float ay, float bx, float by)
    {
        A = new(ax, ay);
        B = new(bx, by);
    }

    public void Draw()
    {
        Raylib.DrawLineEx(A * Const.Meter, B * Const.Meter, 5, Color.BLACK);
    }
}