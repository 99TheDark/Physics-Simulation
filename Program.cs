Simulation sim = new(1080, 720, "Physics Simulation");

List<Line> bezier = Line.Bezier(1f, 1f, 8f, 5f, 1.5f, 2.5f, 3.5f, 5f);
sim.Lines.AddRange(bezier);
sim.Lines.Add(new(8f, 5f, 12f, 5f));
sim.Lines.Add(new(15f, 4f, 9f, 10f));

sim.Balls.Add(new(3f, 0.3f, 2f, 0.2f));

sim.Run();