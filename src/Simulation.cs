using System.Numerics;
using Raylib_cs;

public class Simulation
{
    public readonly int Width;
    public readonly int Height;
    public readonly string Title;

    public readonly int Iterations;

    public List<Ball> Balls;
    public List<Line> Lines;

    private readonly System.Timers.Timer timer;
    private readonly List<Ball> ballQueue;
    private readonly List<Line> lineQueue;

    public Simulation(int width, int height, string title, int iterations = 10)
    {
        Width = width;
        Height = height;
        Title = title;
        Iterations = iterations;

        Balls = new();
        Lines = new();

        timer = new(3000);
        ballQueue = new();
        lineQueue = new();
    }

    private void CallBalls(Action<Ball> act)
    {
        for (int i = 0; i < Balls.Count; i++)
        {
            act(Balls[i]);
        }
    }

    private void CallLines(Action<Line> act)
    {
        for (int i = 0; i < Lines.Count; i++)
        {
            act(Lines[i]);
        }
    }

    public void Run()
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.LOG_ERROR);
        Raylib.InitWindow(Width, Height, Title);

        Random rand = new Random();

        timer.AutoReset = true;
        timer.Elapsed += new((_, _) => ballQueue.Add(
            new(Utils.RandomRange(rand, 0.1f, 0.5f), 1.5f, 0f)
        ));
        timer.Start();

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
        Const.DeltaTime = Raylib.GetFrameTime() / Iterations;

        for (int i = 0; i < Iterations; i++)
        {
            CallBalls(ball =>
            {
                ball.Update();
            });

            CallBalls(ball =>
            {
                // TODO: Check for collisions first, and only include them
                CallBalls(b =>
                {
                    if (ball == b)
                    {
                        return;
                    }

                    ball.Collide(b);
                });
            });

            CallBalls(ball =>
            {
                CallLines(line =>
                {
                    ball.Collide(line);
                });
            });

            CallBalls(ball =>
            {
                ball.Step();
            });

            CallBalls(ball =>
            {
                if (Ball.Invalid(ball) || ball.Position.Y >= 30) Balls.Remove(ball);
            });
        }

        foreach (Ball ball in ballQueue.ToList())
        {
            Balls.Add(ball);
        }

        foreach (Line line in lineQueue.ToList())
        {
            Lines.Add(line);
        }

        ballQueue.Clear();
        lineQueue.Clear();
    }

    public void Draw()
    {
        CallLines(line =>
        {
            line.Draw();
        });

        CallBalls(ball =>
        {
            ball.Draw();
        });
    }
}