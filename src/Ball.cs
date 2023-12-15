using System.Numerics;
using Raylib_cs;

public class Ball : Renderable
{
    public readonly Color Color;

    public readonly float Mass;
    public readonly float Radius;
    public readonly float Diameter;

    public Vector2 Position;
    public Vector2 Velocity = Vector2.Zero;
    public Vector2 Acceleration = Vector2.Zero;

    public Ball(float radius, Vector2 position)
    {
        Mass = (float) (Math.PI * radius * radius);
        Radius = radius;
        Diameter = radius * 2;
        Position = position;
        Color = Utils.VibrantColor();
    }

    public Ball(float radius, float xPosition, float yPosition)
    {
        Mass = (float) Math.PI * radius * radius;
        Radius = radius;
        Diameter = radius * 2;
        Position = new(xPosition, yPosition);
        Color = Utils.VibrantColor();
    }

    public static bool Invalid(Ball ball)
    {
        return double.IsNaN(ball.Position.X) || double.IsNaN(ball.Position.Y);
    }

    public void Update()
    {
        Acceleration = Vector2.Zero;

        // Gravity
        Acceleration.Y += Const.Gravity;

        // Drag: diameter = cross sectional area (a line)
        Acceleration.Y -= Const.Drag * Diameter * Velocity.LengthSquared() / Mass;
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

            // TODO: Apply friction using ApplyNormal once velocity is sorted out
            ApplyForce(normal);
            ball.ApplyForce(-normal);
        }
    }

    public void ApplyForce(Vector2 force)
    {
        Acceleration += force / Mass;
    }

    public void ApplyNormal(Vector2 normal)
    {
        if (normal == Vector2.Zero) return;

        ApplyForce(normal);

        Velocity -= Utils.Project(Velocity, normal) * Const.StaticFriction;

        Vector2 parallel = new(normal.Y, -normal.X);
        Vector2 direction = Utils.Project(Velocity, parallel);

        if (direction != Vector2.Zero)
        {
            Vector2 opposition = -Vector2.Normalize(direction);
            Vector2 friction = opposition * normal.Length() * Const.KineticFriction;

            ApplyForce(friction);
        }
    }

    // https://stackoverflow.com/questions/849211/shortest-distance-between-a-point-and-a-line-segment
    public (Vector2 exit, Vector2 normal) Normal(Line line)
    {
        float length2 = Vector2.DistanceSquared(line.A, line.B);
        if (length2 == 0)
        {
            return (Vector2.Zero, Vector2.Zero);
        }

        float dot = Vector2.Dot(Position - line.A, line.Difference);

        Vector2 displacement = Position - (line.A + Utils.Clamp01(dot / length2) * line.Difference);

        if (displacement.Length() > Radius)
        {
            return (Vector2.Zero, Vector2.Zero);
        }

        Vector2 normalized = Vector2.Normalize(displacement);

        Vector2 exit = normalized * (Radius - displacement.Length());

        dot = Vector2.Dot(Position - line.A, line.Difference);
        displacement = Position - (line.A + Utils.Clamp01(dot / length2) * line.Difference);
        normalized = Vector2.Normalize(displacement);

        Vector2 normal = normalized * Mass * Utils.Project(Acceleration, normalized).Length();

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