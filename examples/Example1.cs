public class Example1 : Example
{
    public override void Setup(Simulation sim)
    {
        sim.Balls.Add(new(0.3f, 2f, 0.2f));
        sim.Balls.Add(new(0.4f, 2.1f, -0.5f));
        sim.Balls.Add(new(0.1f, 1.8f, -0.8f));

        sim.Lines.Add(new(1.5f, 1f, 2f, 2.2f));
        sim.Lines.Add(new(2.9f, 1f, 2f, 2.2f));
    }
}