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

    public static List<Line> Bezier(float x1, float y1, float x2, float y2,
                                    float cx1, float cy1, float cx2, float cy2, int subdivisions = 300)
    {
        return Bezier(new(x1, y1), new(x2, y2), new(cx1, cy1), new(cx2, cy2), subdivisions);
    }

    // https://www.khanacademy.org/computer-programming/thedarks-clash-of-code-entry/6066684096200704
    public static List<Line> Bezier(Vector2 p1, Vector2 p2, Vector2 c1, Vector2 c2, int subdivisions = 40)
    {
        List<Vector2> points = new();
        for (int i = 0; i <= subdivisions; i++)
        {
            // B(t) = (1 - t)^3*P0 + 3(1-t)^2*t*P1 + 3(1 - t)*t^2*P2 + t^3*P3, cubic bezier
            float t = (float) i / subdivisions;

            // TODO: Simplify & format
            Vector2 point = Utils.Pow(1 - t, 3) * p1 + 3 * Utils.Pow(1 - t, 2) * t * c1 + 3 * (1 - t) * Utils.Pow(t, 2) * c2 + Utils.Pow(t, 3) * p2;

            points.Add(point);
        }

        List<Line> lines = new();
        for (int i = 0; i < points.Count - 1; i++)
        {
            Line line = new(points[i], points[i + 1]);
            lines.Add(line);
        }

        return lines;
    }

    public void Draw()
    {
        Raylib.DrawLineEx(A * Const.Meter, B * Const.Meter, Const.LineThickness, Color.BLACK);
    }
}