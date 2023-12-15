Simulation sim = new(1080, 720, "Physics Simulation");

sim.Lines.Add(new(1f, 1f, 1f, 6f));
sim.Lines.Add(new(1f, 6f, 10f, 6f));
sim.Lines.Add(new(10f, 6f, 10f, 1f));

sim.Run();