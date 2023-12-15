using System.Diagnostics;
using Raylib_cs;

public class Simulation
{
    public int Width;
    public int Height;
    public string Title;

    public List<Ball> Balls;
    public List<Line> Lines;

    public Simulation(int width, int height, string title)
    {
        Width = width;
        Height = height;
        Title = title;

        Balls = new();
        Lines = new();
    }

    public void Run()
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.LOG_ERROR);
        Raylib.InitWindow(Width, Height, Title);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            Simulate();
            Draw();

            Raylib.DrawText($"FPS: {Math.Round(1 / Const.DeltaTime, 1)}", 10, 10, 16, Color.BLACK);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    public void Simulate()
    {
        Const.DeltaTime = Raylib.GetFrameTime();

        List<Ball> Invalid = new();

        // TODO: Apply one kind of force at a time
        foreach (Ball ball in Balls)
        {
            ball.Update();

            // TODO: Check for collisions first, and only include them
            foreach (Ball b in Balls)
            {
                if (b == ball)
                {
                    continue;
                }

                ball.Collide(b);
            }

            foreach (Line l in Lines)
            {

                ball.Collide(l);
            }

            ball.Step();

            if (Ball.Invalid(ball)) Invalid.Add(ball);
        }

        foreach (Ball ball in Invalid)
        {
            Balls.Remove(ball);
        }
    }

    public void Draw()
    {
        foreach (Line line in Lines)
        {
            line.Draw();
        }

        foreach (Ball ball in Balls)
        {
            ball.Draw();
        }
    }
}