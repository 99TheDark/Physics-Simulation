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
        Raylib.InitWindow(Width, Height, Title);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            Const.DeltaTime = Raylib.GetFrameTime();

            Simulate();
            Draw();

            Raylib.DrawText($"FPS: {Math.Round(1 / Const.DeltaTime, 1)}", 10, 10, 16, Color.BLACK);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    public void Simulate()
    {
        // TODO: Apply one kind of force at a time
        foreach (Ball ball in Balls)
        {
            ball.Update();

            // TODO: Skip if rectangle bounds is not touching circle: Maybe checking circle-line & circle-circle collisions will be faster than also including them
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