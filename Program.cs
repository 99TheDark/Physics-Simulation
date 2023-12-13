Simulation sim = new(1080, 720, "Physics Simulation");

sim.Lines.Add(new(1f, 1f, 1f, 7f));
sim.Lines.Add(new(1f, 7f, 8f, 6f));
sim.Lines.Add(new(8f, 6f, 8f, 1f));

Random rand = new();
for (int i = 0; i < 8; i++)
{
    float x = RandomRange(rand, 2.5f, 6.5f);
    float y = 6 - i;
    float radius = RandomRange(rand, 0.1f, 1f);
    float mass = (float) Math.PI * radius * radius;

    Ball ball = new(mass, radius, x, y);
    sim.Balls.Add(ball);
}

float RandomRange(Random random, float min, float max)
{
    return (float) random.NextDouble() * (max - min) + min;
}

sim.Run();