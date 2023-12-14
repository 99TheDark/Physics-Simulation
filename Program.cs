Simulation sim = new(1080, 220, "Physics Simulation");

// Velocity is never opposed by normal force — needs to fix
List<Line> bezier = Line.Bezier(1f, 1f, 8f, 5f, 1.5f, 2.5f, 3.5f, 5f);
sim.Lines.AddRange(bezier);
sim.Lines.Add(new(8f, 5f, 26.92f, 5f));

sim.Balls.Add(new(3f, 0.3f, 2f, 0.2f));

sim.Run();