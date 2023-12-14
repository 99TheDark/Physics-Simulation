Simulation sim = new(1080, 720, "Physics Simulation");

/*sim.Lines.Add(new(1f, 1f, 1f, 7f));
sim.Lines.Add(new(1f, 7f, 8f, 6f));
sim.Lines.Add(new(8f, 6f, 8f, 1f));

Random rand = new();
for (int i = 0; i < 8; i++)
{
    float x = Utils.RandomRange(rand, 2.5f, 6.5f);
    float y = 6 - i;
    float radius = Utils.RandomRange(rand, 0.1f, 1f);
    float mass = (float) Math.PI * radius * radius;

    Ball ball = new(mass, radius, x, y);
    sim.Balls.Add(ball);
}*/

List<Line> bezier = Line.Bezier(1f, 1f, 8f, 5f, 1.5f, 2.5f, 3.5f, 5f);
sim.Lines.AddRange(bezier);
sim.Lines.Add(new(8f, 5f, 14f, 5f));

sim.Balls.Add(new(3f, 0.3f, 2f, 1.5f));

sim.Run();