Simulation sim = new(1080, 720, "Physics Simulation");

/*sim.Lines.Add(new(1f, 1f, 1f, 6f));
sim.Lines.Add(new(1f, 6f, 10f, 6f));
sim.Lines.Add(new(10f, 6f, 10f, 1f));

sim.Balls.Add(new(0.2f, 5f, 5f));
sim.Balls.Add(new(0.4f, 5f, 3.5f));
sim.Balls.Add(new(0.6f, 5f, 0f));

/*for (float y = 0f; y <= 5f; y += 0.3f)
{
    for (float x = 1.1f; x <= 9.9f; x += 0.3f)
    {
        sim.Balls.Add(new(0.1f, x, y));
    }
}*/

Example3.Setup(sim);

sim.Run();