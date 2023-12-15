﻿Simulation sim = new(1080, 720, "Physics Simulation");

sim.Lines.AddRange(Line.Bezier(1f, 1f, 8f, 5f, 1.5f, 2.5f, 3.5f, 5f));
sim.Lines.Add(new(8f, 5f, 12f, 5f));
sim.Lines.Add(new(15f, 4f, 9f, 10f));
sim.Lines.AddRange(Line.Bezier(9f, 10f, 6f, 9.5f, 7.5f, 11.5f, 6f, 9.5f));
sim.Lines.Add(new(1f, 7.5f, 1f, 8.5f));
sim.Lines.Add(new(1f, 8.5f, 2f, 8.3f));

sim.Balls.Add(new(3f, 0.4f, 2f, 0.2f));
sim.Balls.Add(new(5f, 0.5f, 8f, 7f));

sim.Run();