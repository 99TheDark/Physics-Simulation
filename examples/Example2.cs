public class Example2 : Example
{
    public override void Setup(Simulation sim)
    {
        sim.Lines.Add(new(1f, 1f, 1f, 7f));
        sim.Lines.Add(new(1f, 7f, 10f, 6f));
        sim.Lines.Add(new(10f, 6f, 10f, 1f));

        Random rand = new();
        for (int i = 0; i < 10; i++)
        {
            float x = Utils.RandomRange(rand, 2.5f, 8.5f);
            float y = 6 - i;
            float radius = Utils.RandomRange(rand, 0.1f, 1f);

            Ball ball = new(radius, x, y);
            sim.Balls.Add(ball);
        }
    }
}