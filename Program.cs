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

List<Line> bezier = Line.Bezier(1f, 1f, 16f, 10f, 3f, 5f, 7f, 10f);
sim.Lines.AddRange(bezier);

sim.Balls.Add(new(3f, 0.3f, 2f, 1f));
sim.Lines.Add(new(1.99f, 2f, 1.99f, 4f));

sim.Run();