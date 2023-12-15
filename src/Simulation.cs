using Raylib_cs;

public class Simulation
{
    public int Width;
    public int Height;
    public string Title;

    public List<Ball> Balls;
    public List<Line> Lines;

    private System.Timers.Timer timer = new(2000);

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

        Random rand = new Random();

        timer.AutoReset = true;
        timer.Elapsed += new((_, _) => Balls.Add(new(Utils.RandomRange(rand, 0.1f, 0.5f), 1.5f, 0f)));
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
        Const.DeltaTime = Raylib.GetFrameTime();

        // TODO: Do something cleaner than spamming ToList, perhaps copy beforehand
        foreach (Ball ball in Balls.ToList())
        {
            ball.Update();

            // TODO: Check for collisions first, and only include them
            foreach (Ball b in Balls.ToList())
            {
                if (b == ball)
                {
                    continue;
                }

                ball.Collide(b);
            }

            foreach (Line l in Lines.ToList())
            {

                ball.Collide(l);
            }

            ball.Step();

            if (Ball.Invalid(ball) || ball.Position.Y >= 500) Balls.Remove(ball);
        }
    }

    public void Draw()
    {
        foreach (Line line in Lines.ToList())
        {
            line.Draw();
        }

        foreach (Ball ball in Balls.ToList())
        {
            ball.Draw();
        }
    }
}