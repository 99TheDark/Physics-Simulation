using System.Numerics;
using Raylib_cs;

public class Ball : Renderable
{
    public Color Color;

    public float Mass;
    public float Radius;

    public Vector2 Position;
    public Vector2 Velocity = Vector2.Zero;
    public Vector2 Acceleration = Vector2.Zero;

    public Ball(float mass, float radius, Vector2 position)
    {
        Mass = mass;
        Radius = radius;
        Position = position;
        Color = Utils.VibrantColor();
    }

    public Ball(float mass, float radius, float xPosition, float yPosition)
    {
        Mass = mass;
        Radius = radius;
        Position = new(xPosition, yPosition);
        Color = Utils.VibrantColor();
    }

    public void Update()
    {
        Acceleration = Vector2.Zero;
        Acceleration.Y += Const.Gravity;
    }

    public void Step()
    {
        Velocity += Acceleration * Const.DeltaTime;
        Position += Velocity * Const.DeltaTime;
    }

    public void Collide(Line line)
    {
        (Vector2 exit, Vector2 normal) = Normal(line);

        if (exit != Vector2.Zero)
        {
            Position += exit;

            ApplyNormal(normal);
        }
    }

    public void Collide(Ball ball)
    {
        (Vector2 exit, Vector2 normal) = Normal(ball);

        if (exit != Vector2.Zero)
        {
            Position += exit;

            ApplyNormal(normal);
            ball.ApplyNormal(-normal);
        }
    }

    public void ApplyForce(Vector2 force)
    {
        Acceleration += force / Mass;
    }

    public void ApplyNormal(Vector2 normal)
    {
        ApplyForce(normal);

        // This isn't correct unless the object is completely unmoving (ie, a line, not a ball)
        // Velocity -= Utils.Project(Velocity, normal);

        Vector2 difference = new(normal.Y, -normal.X);

        Vector2 opposition = -Vector2.Normalize(Utils.Project(Velocity, difference));
        Vector2 friction = opposition * normal.Length() * Const.KineticFriction;

        if (Mass > 4) Console.WriteLine(normal);

        ApplyForce(friction);
    }

    // https://stackoverflow.com/questions/849211/shortest-distance-between-a-point-and-a-line-segment
    public (Vector2 exit, Vector2 normal) Normal(Line line)
    {
        float length2 = Vector2.DistanceSquared(line.A, line.B);
        if (length2 == 0)
        {
            return (Vector2.Zero, Vector2.Zero);
        }

        Vector2 difference = line.B - line.A;
        float dot = Vector2.Dot(Position - line.A, difference);

        Vector2 displacement = Position - (line.A + Utils.Clamp01(dot / length2) * difference);

        if (displacement.Length() > Radius)
        {
            return (Vector2.Zero, Vector2.Zero);
        }

        Vector2 normalized = Vector2.Normalize(displacement);

        Vector2 exit = normalized * (Radius - displacement.Length());

        dot = Vector2.Dot(Position - line.A, difference);
        displacement = Position - (line.A + Utils.Clamp01(dot / length2) * difference);
        normalized = Vector2.Normalize(displacement);

        Vector2 normal = normalized * Mass * Acceleration.Length();

        if (Mass > 4) Console.WriteLine(normal);

        return (exit, normal);
    }

    public (Vector2 exit, Vector2 normal) Normal(Ball ball)
    {
        float distance = Vector2.Distance(Position, ball.Position);
        float interception = Radius + ball.Radius - distance;

        if (interception > 0)
        {
            Vector2 normalized = (Position - ball.Position) / distance;

            Vector2 exit = normalized * interception;
            Vector2 normal = normalized * Mass * Acceleration.Length(); // same thing as bal

            return (exit, normal);
        }

        return (Vector2.Zero, Vector2.Zero);
    }

    public void Draw()
    {
        Raylib.DrawCircle(
            (int) (Position.X * Const.Meter),
            (int) (Position.Y * Const.Meter),
            Radius * Const.Meter,
            Color
        );

        if (Const.ForceArrows) Raylib.DrawLineEx(
            Position * Const.Meter,
            Position * Const.Meter + Acceleration * Mass,
            Const.LineThickness,
            Color.RED
        );
    }
}